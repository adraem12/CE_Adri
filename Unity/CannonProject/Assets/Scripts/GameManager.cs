using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Variables
    public GameObject cannonBallPrefab;
    public GameObject bullseyePrefab;
    public CrossScript cross;
    [HideInInspector] public CannonScript cannon;
    public Controls controls;
    [HideInInspector] public int hits = -1;
    [HideInInspector] public int shots = 0;

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
        Instance = this; // Crea la instancia del Game Manager
    }

    private void Start()
    {
        SpawnBullseye(); // Crea la primera diana
    }

    private void Update()
    {
        
    }

    public void ButtonDown() // Cuando se clicka
    {
        cannon.StartCoroutine(cannon.ChargeCannon());
    }

    public void ButtonUp() // Cuando se deja de clickar
    {
        cannon.StopAllCoroutines();
        Shoot();
    }

    public void Shoot() // Acabar disparo cargado
    {
        cannon.Shoot();
        shots++;
        UIManager.Instance.ShotUI(shots);
    }

    public void SpawnBullseye() // Genera una nueva diana
    {
        Vector3 newSpawn = new(Random.Range(cross.GetMaxMoveX().x, cross.GetMaxMoveX().y), Random.Range(cross.GetMaxMoveY().x, cross.GetMaxMoveY().y), cross.transform.position.z);
        Instantiate(bullseyePrefab, newSpawn, Quaternion.identity);
        hits++;
        UIManager.Instance.HitUI(hits);
    }
}