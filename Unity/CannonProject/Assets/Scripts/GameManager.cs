using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Variables
    public GameObject cannonBallPrefab;
    public GameObject bullseyePrefab;
    public CrossScript cross;
    public CannonScript cannon;
    public Controls controls;
    [HideInInspector] public int hits = 0;
    [HideInInspector] public int shots = 0;
    public int timeLeft = 0;
    bool hardMode;

    private void OnEnable() // Activa los controles
    {
        controls.Enable();
    }

    private void OnDisable() // Desactiva los controles
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new();
        Instance = this; // Crea la instancia del Game Manager
    }

    public void ButtonDown() // Cuando se clicka
    {
        cannon.StartCoroutine(cannon.ChargeCannon());
    }

    public void ButtonUp() // Cuando se deja de clickar
    {
        cannon.StopAllCoroutines();
        cannon.Shoot();
        shots++;
    }

    public void SpawnBullseye(bool hit, Vector3 lastPosition) // Genera una nueva diana
    {
        float maxDistance;
        int newTimer;
        if (hit) hits++;
        if (!hardMode)
        {
            maxDistance = 3f;
            newTimer = 5;
            if (hit) timeLeft += 3;
        }
        else
        {
            maxDistance = 8;
            newTimer = 3;
            if (hit) timeLeft += 1;
        }
        float newX = Mathf.Clamp(Random.Range(lastPosition.x - maxDistance, lastPosition.x + maxDistance), cross.GetMaxMoveX().x, cross.GetMaxMoveX().y);
        float newY = Mathf.Clamp(Random.Range(lastPosition.y - maxDistance, lastPosition.y + maxDistance), cross.GetMaxMoveY().x, cross.GetMaxMoveY().y);
        Instantiate(bullseyePrefab, new Vector3(newX, newY, cross.transform.position.z), Quaternion.identity).GetComponent<BullseyeScript>().timer = newTimer;
        UIManager.Instance.TimeUI(timeLeft);
    }

    public IEnumerator Timer(int timeInSeconds)
    {
        timeLeft = timeInSeconds;
        while (timeLeft >= 0)
        {
            UIManager.Instance.TimeUI(timeLeft);
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        GameOver();
    }

    void GameOver()
    {
        cannon.gameObject.SetActive(false);
        cross.gameObject.SetActive(false);
        Destroy(FindAnyObjectByType<BullseyeScript>().gameObject);
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length > 0 )
            foreach (var ball in balls) 
                Destroy(ball);
        if (hits >= 10 && hits >= shots/2)
            UIManager.Instance.GameUIOff(true);
        else
            UIManager.Instance.GameUIOff(false);
    }

    public void StartGame()
    {
        hardMode = UIManager.Instance.hardModeToggle.isOn;
        if (hardMode)
            StartCoroutine(Timer(15));
        else
            StartCoroutine(Timer(20));
        cannon.gameObject.SetActive(true);
        cross.gameObject.SetActive(true);
        UIManager.Instance.GameUIOn();
        SpawnBullseye(false, new Vector3(0, 10, -20)); // Crea la primera diana
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}