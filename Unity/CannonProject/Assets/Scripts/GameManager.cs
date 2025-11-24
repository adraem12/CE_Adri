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
    [HideInInspector] public int hits = -1;
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

    public void SpawnBullseye() // Genera una nueva diana
    {
        Vector3 newSpawn = new(Random.Range(cross.GetMaxMoveX().x, cross.GetMaxMoveX().y), Random.Range(cross.GetMaxMoveY().x, cross.GetMaxMoveY().y), cross.transform.position.z);
        Instantiate(bullseyePrefab, newSpawn, Quaternion.identity);
        hits++;
        if (!hardMode)
            timeLeft += 3;
        else
            timeLeft += 1;
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
        SpawnBullseye(); // Crea la primera diana
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}