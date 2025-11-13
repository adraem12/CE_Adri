using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI ballText;
    public TextMeshProUGUI hitText;
    public GameObject cannonBallPrefab;
    public GameObject bullseyePrefab;
    public Transform cross;
    CannonScript cannon;
    [HideInInspector] public List<GameObject> cannonBallList = new();
    public Controls controls;
    readonly List<Transform> spawns = new();
    int hits = 0;

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
        ballText.text = "Balas: 0";
        hitText.text = "Hits: 0";
        SpawnBullseye(0); // Crea la primera diana
    }

    public void Button1() // Disparo normal
    {
        cannon.Shoot(false);
        ballText.text = "Balas: " + cannonBallList.Count;
    }

    public void Button2() // Borrar balas
    {
        foreach (var ball in cannonBallList)
            Destroy(ball);
        cannonBallList.Clear();
        ballText.text = "Balas: " + cannonBallList.Count;
    }

    public void Button3() // Disparo aleatorio
    {
        cannon.Shoot(true);
        ballText.text = "Balas: " + cannonBallList.Count;
    }

    public void SpawnBullseye(int oldIndex) // Genera una nueva diana
    {
        int newIndex = Random.Range(1, spawns.Count + 1);
        while (newIndex == oldIndex) // Spawn aleatorio que no se repite
            newIndex = Random.Range(1, spawns.Count + 1);
        Transform newParent = spawns[newIndex - 1];
        Instantiate(bullseyePrefab, newParent);
        ballText.text = "Balas: " + cannonBallList.Count;
        hits++;
        hitText.text = "Hits: " + hits;
    }
}