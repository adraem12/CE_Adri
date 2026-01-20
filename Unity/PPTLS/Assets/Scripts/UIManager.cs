using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject menuPanel;
    public GameObject endPanel;
    public GameObject gamePanel;
    public GameObject actionsPanel;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI turnText;

    private void Awake()
    {
        instance = this;
    }

    public void SetTurnText(string newString)
    {
        turnText.text = newString;
    }

    public void ActivatePlayerHand()
    {
        actionsPanel.SetActive(true);

    }

    public void GameEnd(bool win)
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        if (win)
            endText.text = "You win!";
        else
            endText.text = "You lose :(";
    }

    // Buttons
    public void PlayButton()
    {
        menuPanel.SetActive(false);
        endPanel.SetActive(false);
        gamePanel.SetActive(true);
        GameManager.instance.StartNewGame();
    }

    public void ExitButton()
    {
        menuPanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}