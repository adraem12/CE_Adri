using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager Instance;
    [Header("GUI")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI ringText;
    public TextMeshProUGUI timeText;
    public Slider dashSlider;
    [Header("UI")]
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI endCoinsText;
    public TextMeshProUGUI endRingsText;
    public TextMeshProUGUI endTimeText;
    public TextMeshProUGUI pointsText;
    [Header("GameObjects")]
    public GameObject menuPanel;
    public GameObject endPanel;
    public GameObject gamePanel;

    private void Awake()
    {
        Instance = this; //Crea la instancia del UI Manager
    }

    public void UpdateText(int text, int value)
    {
        switch (text)
        {
            case 1:
                coinText.text = value.ToString();
                break;
            case 2:
                ringText.text = value.ToString() + "/" + RingManager.Instance.RingList().Count;
                break;
            case 3:
                int minutes = Mathf.FloorToInt(value / 60);
                int seconds = Mathf.FloorToInt(value % 60);
                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                break;
            case 4:
                coinText.text = "0";
                ringText.text = "0/" + RingManager.Instance.RingList().Count;
                timeText.text = "00:00";
                DashSliderUpdate(1, 1, true);
                break;
        }
    }

    public void DashSliderUpdate(float maxValue, float currentValue, bool fuel)
    {
        //Controla el slider del impulso
        if (dashSlider.maxValue != maxValue)
            dashSlider.maxValue = maxValue;
        dashSlider.value = currentValue;
    }

    //Lógica de los botones del juego
    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        GameManager.Instance.StartGame();
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void GameOverUI(bool win, int timeLeft, int coins)
    {
        if (win)
        {
            victoryText.text = "You win!";
            pointsText.text = "Points: " + (timeLeft + (coins * 2)).ToString();
        }
        else
        {
            victoryText.text = "You lose :(";
            pointsText.text = "Points: 0";
        }
        endCoinsText.text = "Coins: " + coinText.text;
        endRingsText.text = "Rings: " + ringText.text;
        endTimeText.text = "Time left: " + timeText.text;
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void ExitButton()
    {
        endPanel.SetActive(false);
        menuPanel.SetActive(true);
        GameManager.Instance.ResetGame();
    }
}