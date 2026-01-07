using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI ringText;
    public TextMeshProUGUI timeText;
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
                break;
        }
    }

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

    public void GameOverUI()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void ExitButton()
    {
        endPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}