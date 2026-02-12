using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject difficultyPanel, gamePanel, endPanel;
    [Header("Texts")]
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI dotsText, enemiesKilledText, timeText;
    [Header("Images")]
    public GameObject winImage;
    public GameObject loseImage;

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
        enemiesKilledText.text = "Enemies killed: " + GameManager.enemiesKilled;
        int seconds = (int)GameManager.timer % 60;
        int minutes = (int)GameManager.timer / 60;
        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void QuitDifficultyButton()
    {
        menuPanel.SetActive(true);
        difficultyPanel.SetActive(false);
    }

    public void PlayButton()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void EasyButton()
    {
        GameManager.instance.StartNewGame(0);
        difficultyPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void HardButton()
    {
        GameManager.instance.StartNewGame(1);
        difficultyPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ReplayButton()
    {
        GameManager.instance.StartNewGame(-1);
        endPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}