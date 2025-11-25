using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // Variables
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI aimText;
    public TextMeshProUGUI forceText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI victoryText;
    public GameObject menuPanel;
    public GameObject creditsPanel;
    public GameObject gamePanel;
    public GameObject endPanel;
    public Slider forceSlider;
    public Toggle hardModeToggle;
    CannonScript cannon;

    private void Awake()
    {
        Instance = this;
        shotsText.text = "Shots: 0";
        hitText.text = "Hits: 0";
    }

    void Start()
    {
        cannon = GameManager.Instance.cannon;
        forceSlider.maxValue = cannon.maxForce;
    }

    void Update()
    {
        forceSlider.value = cannon.currentForce; // Actualiza la barra de disparo
        forceText.text = Mathf.Floor(cannon.currentForce).ToString();
    }

    public void TimeUI(int i)
    {
        int minutes = Mathf.FloorToInt(i / 60);
        int seconds = Mathf.FloorToInt(i % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameUIOff(bool win)
    {
        gamePanel.SetActive(false);
        shotsText.text = "Shots: " + GameManager.Instance.shots;
        hitText.text = "Hits: " + GameManager.Instance.hits;
        aimText.text = "Aim : " + (Mathf.Round((float)GameManager.Instance.hits / (float)GameManager.Instance.shots * 10000f) / 100f) + "%";
        if (win)
            victoryText.text = "You win!";
        else
            victoryText.text = "You lose :(";
        endPanel.SetActive(true);
    }

    public void GameUIOn()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void MenuUIOn()
    {
        endPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void CreditsUIOn()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CreditsUIOff()
    {
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}