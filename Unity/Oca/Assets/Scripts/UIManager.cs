using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI roundText, turnText, diceText;
    public Button diceButton, minusButton, plusButton;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateTexts(int round, int turn, int dice)
    {
        roundText.text = "Round " + round;
        if (turn == 0) turnText.text = "Player Turn";
        else turnText.text = "AI Turn";
        diceText.text = dice.ToString();
    }

    public void MinusPlusButtonsEnabled(bool enabled)
    {
        minusButton.gameObject.SetActive(enabled);
        plusButton.gameObject.SetActive(enabled);
    }

    public void DiceButton()
    {
        int a = GameManager.Dice();
        GameManager.instance.playerDice = a;
        diceText.text = a.ToString();
    }

    public void CollisionButton(int i)
    {
        GameManager.instance.playerDice = i;
    }
}