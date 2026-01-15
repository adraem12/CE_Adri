using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject endPanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayButton()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ExitButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}