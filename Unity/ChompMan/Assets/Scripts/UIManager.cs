using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void PlayButton()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReplayButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}