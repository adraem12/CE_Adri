using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SceneObjects")]
    public static GameManager instance;
    public GameObject player;
    public Transform spawnTransform;
    public Controls controls;

    private void Awake()
    {
        instance = this;
        controls = new();
        controls.Enable();
    }

    public void GameOver()
    {
        controls.Disable();
        UIManager.Instance.GameOver();
    }
}