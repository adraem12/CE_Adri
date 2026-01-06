using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject coinPrefab;
    public Controls controls;
    public GameObject ship;
    public SplineInstancesScript splineInstancesScript;
    int coins = 0;
    int rings = 0; 

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

    void Start()
    {
        List<int> indexedCoins = new();
        int index;
        for (int i = 0; i < 15; i++) 
        {
            do
                index = Random.Range(0, 26);
            while (indexedCoins.Contains(index));
            indexedCoins.Add(index);
            Instantiate(coinPrefab, splineInstancesScript.SpawnerList().ToArray()[index].transform.position, Quaternion.identity, splineInstancesScript.transform.GetChild(2));
        }
        for (int i = 0; i < 5; i++)
        {
            do
                index = Random.Range(26, 41);
            while (indexedCoins.Contains(index));
            indexedCoins.Add(index);
            Instantiate(coinPrefab, splineInstancesScript.SpawnerList().ToArray()[index].transform.position, Quaternion.identity, splineInstancesScript.transform.GetChild(2));
        }
    }

    public void AddCoin()
    {
        coins++;
    }

    public void AddRing() 
    {
        rings++;
    }
}