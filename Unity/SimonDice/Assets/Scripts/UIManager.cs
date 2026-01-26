using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button[] buttons;
    public GameObject replayPanel;
    public TextMeshProUGUI currentRoundText;
    public TextMeshProUGUI recordRoundText;

    private void Awake()
    {
        Instance = this;
    }

    public void ReproduceColor(int button)
    {
        buttons[button].GetComponent<Animator>().SetTrigger("Activate");
    }

    public void GameEnd()
    {
        if (GameManager.Instance.currentRound > GameManager.maxRound)
            GameManager.maxRound = GameManager.Instance.currentRound;
        currentRoundText.text = "Current round: 0";
        recordRoundText.text = "Record round: " + GameManager.maxRound;
    }

    public void ActivateButtons(bool activate)
    {
        foreach (Button button in buttons)
            button.interactable = activate;
    }

    // Buttons
    public void ReplayButton()
    {
        GameManager.Instance.StartNewGame();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}