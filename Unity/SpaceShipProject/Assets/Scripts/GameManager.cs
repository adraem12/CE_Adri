using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject coinPrefab;
    public Controls controls;
    public GameObject ship;
    public RingManager ringManager;
    Vector3 initPosition;
    int timeLeft = 0;
    int coins = 0;
    int rings = 0; 

    private void OnEnable() //Activa los controles
    {
        controls.Enable();
    }

    private void OnDisable() //Desactiva los controles
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new();
        Instance = this; //Crea la instancia del Game Manager
        initPosition = ship.transform.position;
    }

    public void StartGame()
    {
        RingManager.Instance.StartGame();
        List<int> indexedCoins = new();
        int index;
        for (int i = 0; i < 15; i++)
        {
            do
                index = Random.Range(0, 26);
            while (indexedCoins.Contains(index));
            indexedCoins.Add(index);
            Instantiate(coinPrefab, ringManager.SpawnerList().ToArray()[index].transform.position, Quaternion.identity, ringManager.transform.GetChild(2));
        }
        for (int i = 0; i < 5; i++)
        {
            do
                index = Random.Range(26, 41);
            while (indexedCoins.Contains(index));
            indexedCoins.Add(index);
            Instantiate(coinPrefab, ringManager.SpawnerList().ToArray()[index].transform.position, Quaternion.identity, ringManager.transform.GetChild(2));
        }
        UIManager.Instance.UpdateText(4, 0);
        StartCoroutine(Timer(60));
    }

    public IEnumerator Timer(int timeInSeconds)
    {
        timeLeft = timeInSeconds;
        while (timeLeft >= 0)
        {
            UIManager.Instance.UpdateText(3, timeLeft);
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        //GameOver();
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
}