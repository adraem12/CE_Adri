using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            turn = 0;
            UIManager.instance.UpdateTexts(round, turn, playerDice);
            yield return new WaitUntil(() => playerDice != 0);
            yield return new WaitForSeconds(0.5f);
            DiceChecker(1, out int startSquare, out int newSquare);
            squareState[startSquare] = 0;
            yield return FigureMove(playerFigure, startSquare, newSquare);
            yield return new WaitForSeconds(0.5f);
            while (squareFunction[newSquare] != 0 || squareState[newSquare] != 0)
            {
                SpecialSquareChecker(1, newSquare, out int specialSquare);
                if (specialSquare == 99)
                {
                    gameEnd = true;
                    yield break;
                }
                if (specialSquare != newSquare)
                {
                    squareState[newSquare] = 0;
                    yield return FigureMove(playerFigure, newSquare, specialSquare);
                    newSquare = specialSquare;
                }
                if (squareState[newSquare] != 0)
                {
                    playerDice = 0;
                    UIManager.instance.MinusPlusButtonsEnabled(true);
                    yield return new WaitUntil(() => playerDice != 0);
                    startSquare = newSquare;
                    newSquare += playerDice;
                    UIManager.instance.MinusPlusButtonsEnabled(false);
                    yield return FigureMove(playerFigure, startSquare, newSquare);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            squareState[newSquare] = 1;
            playerDice = 0;
            yield return new WaitForSeconds(0.5f);
            //AI
            turn = 1;
            rivalDice = Dice();
            UIManager.instance.UpdateTexts(round, turn, rivalDice);
            DiceChecker(2, out startSquare, out newSquare);
            squareState[startSquare] = 0;
            yield return FigureMove(aiFigure, startSquare, newSquare);
            yield return new WaitForSeconds(0.5f);
            while (squareFunction[newSquare] != 0 || squareState[newSquare] != 0)
            {
                SpecialSquareChecker(2, newSquare, out int specialSquare);
                if (specialSquare == 99)
                {
                    gameEnd = true;
                    yield break;
                }
                if (specialSquare != newSquare)
                {
                    squareState[newSquare] = 0;
                    yield return FigureMove(aiFigure, newSquare, specialSquare);
                    newSquare = specialSquare;
                }
                if (squareState[newSquare] != 0)
                {
                    int newDice = squareFunction[newSquare + 1] >= squareFunction[newSquare - 1] ? 1 : -1;
                    startSquare = newSquare;
                    newSquare += newDice;
                    yield return FigureMove(aiFigure, startSquare, newSquare);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            squareState[newSquare] = 2;
            rivalDice = 0;
            round++;
            UIManager.instance.diceButton.interactable = true;
        }
    }

    IEnumerator FigureMove(RectTransform figure, int startSquare, int endSquare)
    {
        List<Vector3> positions = new() { figure.localPosition };
        if (startSquare == 0)
            positions.Add(squareObjects[0].localPosition);
        if (startSquare < endSquare)
            for (int i = startSquare + 1; i <= endSquare; i++) 
                positions.Add(squareObjects[i].localPosition);
        else
            for (int i = startSquare - 1; i >= endSquare; i--)
                positions.Add(squareObjects[i].localPosition);
        int currentObjective = 1;
        float percentage = 0;
        while (Vector3.Distance(figure.localPosition, squareObjects[endSquare].localPosition) > 0.1f)
        {
            percentage += Time.deltaTime * 2f;
            figure.localPosition = Vector3.Slerp(positions[currentObjective - 1], positions[currentObjective], percentage);
            if (Vector3.Distance(figure.localPosition, positions[currentObjective]) < 0.05f)
            {
                percentage = 0;
                currentObjective++;
            }
            yield return null;
        }
    }

    void DiceChecker(int player, out int startSquare, out int endSquare)
    {
        endSquare = 0;
        startSquare = 0;
        if (squareFunction[20] == 98)
        {
            if (player == 1)
                endSquare = playerDice - 1;
            else
            {
                squareFunction[20] = 99;
                endSquare = rivalDice - 1;
            }
        }
        else
        {
            int dice;
            if (player == 1)
                dice = playerDice;
            else
                dice = rivalDice;
            for (int i = 0; i < squareState.Length; i++)
                if (squareState[i] == player)
                {
                    squareState[i] = 0;
                    startSquare = i;
                    if (i + dice < squareState.Length)
                        endSquare = i + dice;
                    else
                        endSquare = i - (dice - (squareState.Length - i));
                    break;
                }
        }
    }

    void SpecialSquareChecker(int player, int oldSquare, out int newSquare)
    {
        newSquare = oldSquare;
        if (squareFunction[oldSquare] != 0)
        {
            squareState[oldSquare] = 0;
            if (squareFunction[oldSquare] == 1)
            {
                if (oldSquare == 0)
                    newSquare = 6;
                else if (oldSquare == 5)
                    newSquare = 12;
            }
            else if (squareFunction[oldSquare] == 2)
            {
                int dice;
                dice = Dice();
                if (player == 1)
                    playerDice = dice;
                else
                    rivalDice = dice;
                squareState[oldSquare] = player;
                DiceChecker(player, out oldSquare, out newSquare);
                UIManager.instance.UpdateTexts(round, turn, dice);
                squareFunction[oldSquare] = 0;
                squareObjects[oldSquare].GetComponent<RawImage>().color = Color.white;
            }
            else if (squareFunction[oldSquare] == -1)
                newSquare = oldSquare - 3;
            else if (squareFunction[oldSquare] == 99)
                newSquare = 99;
        }
    }

    public static int Dice()
    {
        return Random.Range(1, 7);
    }
}