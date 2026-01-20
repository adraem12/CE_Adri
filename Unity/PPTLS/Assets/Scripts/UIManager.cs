using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject menuPanel, endPanel, gamePanel, actionsPanel;
    public TextMeshProUGUI endText, infoText, roundsPText, roundsRText;
    public Texture2D[] handImages;
    public RawImage playerImage, rivalImage;

    private void Awake()
    {
        instance = this;
    }

    public void SetInfoText(string newString)
    {
        infoText.text = newString;
    }

    public void ActiveActionButton(bool active)
    {
        actionsPanel.SetActive(active);
    }

    public void SetRoundText(int playerRounds, int rivalRounds)
    {
        roundsPText.text = playerRounds.ToString() + " / " + GameManager.roundsToWin.ToString();
        roundsRText.text = rivalRounds.ToString() + " / " + GameManager.roundsToWin.ToString();
    }

    public void ShowHands(HandType playerHand, HandType rivalHand, bool active)
    {
        if (active)
        {
            playerImage.texture = handImages[(int)playerHand - 1];
            rivalImage.texture = handImages[(int)rivalHand - 1];
            playerImage.transform.parent.gameObject.SetActive(true);
            rivalImage.transform.parent.gameObject.SetActive(true);
        }
        else 
        {
            playerImage.transform.parent.gameObject.SetActive(false);
            rivalImage.transform.parent.gameObject.SetActive(false);
        }
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

    public void ActionButtons(int num)
    {
        GameManager.instance.playerCurrentHand = (HandType)num;
    }
}