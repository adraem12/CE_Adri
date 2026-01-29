using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject menuPanel, gamePanel, endPanel;
    public TextMeshProUGUI enemiesText, dotsText;
    public GameObject winImage, loseImage;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateDotsText()
    {
        dotsText.text = "DOTS LEFT: " + GameManager.dotsLeft;
        if (GameManager.dotsLeft == 0)
            GameManager.instance.GameOver(true);
    }

    public void UpdateEnemiesText()
    {
        enemiesText.text = "ENEMIES LEFT: " + GameManager.enemiesLeft;
    }

    public void GameOver(bool win)
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        if (win) 
        {
            winImage.SetActive(true);
            loseImage.SetActive(false);
        }
        else
        {
            winImage.SetActive(false);
            loseImage.SetActive(true);
        }
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