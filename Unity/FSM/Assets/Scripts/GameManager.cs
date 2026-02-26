using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Controls controls;
    public GameObject player;

    private void Awake()
    {
        instance = this;
        controls = new();
        controls.Enable();
    }
}