using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject difficultyPanel, gamePanel, endPanel;
    [Header("Texts")]
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI dotsText, enemiesKilledText, timeText, scoreText;
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

    public void GameOver(bool win) //Controls end menu
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        if (win) 
        {
            winImage.SetActive(true);
            loseImage.SetActive(false);
            scoreText.gameObject.SetActive(true);
            SetScore();
        }
        else
        {
            winImage.SetActive(false);
            loseImage.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        enemiesKilledText.text = "Enemies killed: " + GameManager.enemiesKilled;
        int seconds = (int)GameManager.timer % 60;
        int minutes = (int)GameManager.timer / 60;
        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SetScore() //Calculate end score
    {
        switch (GameManager.lastDifficulty)
        {
            case 0: //Easy
                switch (GameManager.timer)
                {
                    case < 30:
                        scoreText.text = "Score: 999";
                        break;
                    case >= 30 and < 60:
                        scoreText.text = "Score: " + (90 + GameManager.enemiesKilled);
                        break;
                    case >= 60 and < 90:
                        scoreText.text = "Score: " + (60 + GameManager.enemiesKilled);
                        break;
                    case >= 90 and < 120:
                        scoreText.text = "Score: " + (30 + GameManager.enemiesKilled);
                        break;
                    case >= 120:
                        scoreText.text = "Score: " + ((int)GameManager.enemiesKilled / 3);
                        break;
                }
                break;
            case 1: //Hard
                switch (GameManager.timer)
                {
                    case < 45:
                        scoreText.text = "Score: 999";
                        break;
                    case >= 45 and < 75:
                        scoreText.text = "Score: " + (90 + GameManager.enemiesKilled);
                        break;
                    case >= 75 and < 105:
                        scoreText.text = "Score: " + (60 + GameManager.enemiesKilled);
                        break;
                    case >= 105 and < 135:
                        scoreText.text = "Score: " + (30 + GameManager.enemiesKilled);
                        break;
                    case >= 135:
                        scoreText.text = "Score: " + ((int)GameManager.enemiesKilled / 3);
                        break;
                }
                break;
        }
    }

    //Button behaviours
    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReturnToMenuButton()
    {
        SceneManager.LoadScene(0);
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

    public void DifficultyButton (int difficulty)
    {
        GameManager.instance.StartNewGame(difficulty);
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