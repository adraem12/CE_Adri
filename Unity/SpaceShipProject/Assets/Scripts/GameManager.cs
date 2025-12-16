using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Controls controls;

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
        
    }

    void Update()
    {
        
    }
}