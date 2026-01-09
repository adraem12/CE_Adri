using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    public static GameManager Instance { get; private set; }
    public Controls controls;
    public GameObject ship;
    public RingManager ringManager;
    public int timer;
    Vector3 initPosition;
    int timeLeft = 0;
    int coins = 0;
    int rings = 0; 

    private void Awake()
    {
        controls = new();
        Instance = this; //Crea la instancia del Game Manager
        initPosition = ship.transform.position;
    }

    public void StartGame()
    {
        //Inicializa el resto de scripts y el crono
        RingManager.Instance.StartGame();
        UIManager.Instance.UpdateText(4, 0);
        StartCoroutine(Timer(timer));
        controls.Enable();
    }

    public IEnumerator Timer(int timeInSeconds)
    {
        timeLeft = timeInSeconds;
        while (timeLeft > 0)
        {
            UIManager.Instance.UpdateText(3, timeLeft);
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        GameOver(false);
    }

    public void AddCoin()
    {
        coins++;
        UIManager.Instance.UpdateText(1, coins);
    }

    public void AddRing() 
    {
        rings++;
        UIManager.Instance.UpdateText(2, rings);
    }

    public void ResetGame() 
    {
        coins = 0;
        rings = 0;
        ship.transform.SetPositionAndRotation(initPosition, new Quaternion(0, 0, 0, 0));
        RingManager.Instance.ResetGame();
    }

    public void GameOver(bool win)
    {
        StopAllCoroutines();
        controls.Disable();
        UIManager.Instance.GameOverUI(win, timeLeft, coins);
    }
}