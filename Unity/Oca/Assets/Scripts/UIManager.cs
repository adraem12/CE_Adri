using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Texts")]
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI turnText, diceText;
    [Header("Prefabs")]
    public Button diceButton;
    public Button minusButton, plusButton;
    public GameObject actionPanel, winPanel;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateTexts(int round, int turn, int dice) //Update canvas info
    {
        roundText.text = "Round " + round;
        if (turn == 1) 
            turnText.text = "Player Turn";
        else 
            turnText.text = "AI Turn";
        if (dice != 0)
        {
            diceText.text = dice.ToString();
            diceButton.GetComponent<Animator>().SetTrigger("dice");
        }
    }

    public void MinusPlusButtonsEnabled(bool enabled) //Control minus and plus buttons
    {
        minusButton.gameObject.SetActive(enabled);
        plusButton.gameObject.SetActive(enabled);
    }

    public void DiceButton() //Dice button behaviour
    {
        int a = GameManager.Dice();
        GameManager.instance.playerDice = a;
        diceText.text = a.ToString();
        diceButton.GetComponent<Animator>().SetTrigger("dice");
    }

    public void SpawnActionPanel(string newText, Vector3 position) //Spawn and position a new action panel
    {
        RectTransform newActionPanel = Instantiate(actionPanel, GameManager.instance.squareObjects[0].parent).GetComponent<RectTransform>();
        newActionPanel.SetLocalPositionAndRotation(position + Vector3.up, Quaternion.identity);
        newActionPanel.GetComponentInChildren<TextMeshProUGUI>().text = newText;
    }

    public void CollisionButton(int i) //Move when two figures collide
    {
        GameManager.instance.playerDice = i;
    }

    public void GameEnd(bool playerWin) //Spawn end panel
    {
        winPanel.SetActive(true);
        if (playerWin)
            winPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You win!";
        else
            winPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You lose :(";
    }
}