using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI forceText;
    public GameObject cannonBallPrefab;
    public GameObject bullseyePrefab;
    public Transform cross;
    CannonScript cannon;
    public Controls controls;
    readonly List<Transform> spawns = new();
    int hits = -1;
    int shots = 0;

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
        cannon = FindFirstObjectByType<CannonScript>();
        for (int i = 0; i < GameObject.Find("Spawns").transform.childCount; i++)
            spawns.Add(GameObject.Find("Spawns").transform.GetChild(i));
        Instance = this; // Crea la instancia del Game Manager
        shotsText.text = "Shots: 0";
        hitText.text = "Hits: 0";
        SpawnBullseye(0); // Crea la primera diana
    }

    private void Update()
    {
        forceText.text = "Force: " + cannon.currentForce.ToString("0.00") + "/3";
    }

    public void Button1Down() // Empezar disparo cargado
    {
        cannon.StartCoroutine(cannon.ChargeCannon());
    }

    public void Button1Up() // Acabar disparo cargado
    {
        cannon.StopAllCoroutines();
        cannon.Shoot();
        shots++;
        shotsText.text = "Shots: " + shots;
    }

    public void SpawnBullseye(int oldIndex) // Genera una nueva diana
    {
        int newIndex = Random.Range(1, spawns.Count + 1);
        while (newIndex == oldIndex) // Spawn aleatorio que no se repite
            newIndex = Random.Range(1, spawns.Count + 1);
        Transform newParent = spawns[newIndex - 1];
        Instantiate(bullseyePrefab, newParent);
        hits++;
        hitText.text = "Hits: " + hits;
    }
}