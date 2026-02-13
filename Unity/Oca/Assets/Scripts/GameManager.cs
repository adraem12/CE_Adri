using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int[] squareState;
    int[] squareFunction;
    public RectTransform[] squareObjects;
    public RectTransform playerFigure, aiFigure;
    int round = 1;
    int turn = 0;
    public int playerDice = 0;
    public int rivalDice = 0;

    private void Awake()
    {
        instance = this;
        squareState = new int[21];
        squareFunction = new int[21];
        for (int i = 0; i < squareState.Length; i++)
            squareState[i] = 0;
        for (int i = 0; i < squareFunction.Length; i++)
            squareFunction[i] = 0;
        //Teleports
        squareFunction[0] = 1;
        squareFunction[5] = 1;
        //Roll again
        squareFunction[11] = 2;
        squareFunction[17] = 2;
        //Move back 3 squares
        squareFunction[4] = -1;
        squareFunction[9] = -1;
        squareFunction[13] = -1;
        squareFunction[18] = -1;
        squareFunction[19] = -1;
        //Win
        squareFunction[20] = 98;
    }

    private void Start()
    {
        UIManager.instance.UpdateTexts(round, turn, 0);
        StartCoroutine(TurnSystem());
    }

    IEnumerator TurnSystem()
    {
        bool gameEnd = false;
        while (!gameEnd) 
        {
            yield return new WaitUntil(() => playerDice != 0);
            yield return new WaitForSeconds(1);
            DiceChecker(1, out int newSquare);
            playerFigure.localPosition = squareObjects[newSquare].localPosition;
            yield return new WaitForSeconds(1);
            while (squareFunction[newSquare] != 0 || squareState[newSquare] != 0)
            {
                SpecialSquareChecker(1, newSquare, out int specialSquare);
                if (specialSquare != newSquare)
                    newSquare = specialSquare;
                playerFigure.localPosition = squareObjects[newSquare].localPosition;
                if (squareState[newSquare] != 0)
                {
                    playerDice = 0;
                    UIManager.instance.MinusPlusButtonsEnabled(true);
                    yield return new WaitUntil(() => playerDice != 0);
                    newSquare += playerDice;
                    UIManager.instance.MinusPlusButtonsEnabled(false);
                    playerFigure.localPosition = squareObjects[newSquare].localPosition;
                    yield return new WaitForSeconds(1);
                }
            }
            squareState[newSquare] = 1;
            playerDice = 0;
            yield return new WaitForSeconds(1);
            rivalDice = Dice();
            DiceChecker(2, out newSquare);
            aiFigure.localPosition = squareObjects[newSquare].localPosition;
            yield return new WaitForSeconds(1);
            while (squareFunction[newSquare] != 0 || squareState[newSquare] != 0)
            {
                SpecialSquareChecker(2, newSquare, out int specialSquare);
                if (specialSquare != newSquare)
                    newSquare = specialSquare;
                aiFigure.localPosition = squareObjects[newSquare].localPosition;
                if (squareState[newSquare] != 0)
                {
                    int newDice = squareFunction[newSquare + 1] >= squareFunction[newSquare - 1] ? 1 : -1;
                    newSquare += newDice;
                    aiFigure.localPosition = squareObjects[newSquare].localPosition;
                    yield return new WaitForSeconds(1);
                }
            }
            squareState[newSquare] = 2;
            rivalDice = 0;
            UIManager.instance.diceButton.interactable = true;
        }
    }

    void DiceChecker(int player, out int newSquare)
    {
        newSquare = 0;
        if (squareFunction[20] == 98)
        {
            if (player == 1)
                newSquare = playerDice - 1;
            else
            {
                squareFunction[20] = 99;
                newSquare = rivalDice - 1;
            }
        }
        else
            for (int i = 0; i < squareState.Length; i++)
                if (squareState[i] == player)
                {
                    squareState[i] = 0;
                    if (player == 1)
                    {
                        if (i + playerDice < squareState.Length)
                            newSquare = i + playerDice;
                        else
                            newSquare = i - (playerDice - (squareState.Length - i));
                        break;
                    }
                    else
                    {
                        if (i + rivalDice < squareState.Length)
                            newSquare = i + rivalDice;
                        else
                            newSquare = i - (rivalDice - (squareState.Length - i));
                        break;
                    }
                }
    }

    void SpecialSquareChecker(int player, int oldSquare, out int newSquare)
    {
        newSquare = oldSquare + 1;
    }

    public static int Dice()
    {
        return Random.Range(1, 7);
    }
}