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
    bool gameEnd;

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
        while (!gameEnd) 
        {
            //Player
            turn = 1;
            UIManager.instance.UpdateTexts(round, turn, 0);
            UIManager.instance.diceButton.interactable = true;
            yield return new WaitUntil(() => playerDice != 0);
            yield return new WaitForSeconds(0.5f);
            yield return Turn(1);
            yield return new WaitForSeconds(0.5f);
            //AI
            turn = 2;
            yield return new WaitForSeconds(0.5f);
            yield return Turn(2);
            round++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Turn(int player)
    {
        turn = player;
        RectTransform figure;
        UIManager.instance.UpdateTexts(round, turn, 0);
        if (player == 2) //Set player variables
        {
            rivalDice = Dice();
            figure = aiFigure;
        }
        else
            figure = playerFigure;
        DiceChecker(player, out int startSquare, out int newSquare);
        if (player == 2)
            UIManager.instance.SpawnActionPanel(rivalDice.ToString(), squareObjects[startSquare].localPosition);
        squareState[startSquare] = 0;
        yield return FigureMove(figure, startSquare, newSquare);
        while (squareFunction[newSquare] != 0 || squareState[newSquare] != 0) //Check for special or full square
        {
            SpecialSquareChecker(player, newSquare, out int specialSquare);
            if (specialSquare == 99) //Win square
            {
                gameEnd = true;
                yield break;
            }
            if (specialSquare != newSquare) //Action if special square
            {
                squareState[newSquare] = 0;
                yield return FigureMove(figure, newSquare, specialSquare);
                newSquare = specialSquare;
            }
            if (squareState[newSquare] != 0) //Full square
            {
                int newDice;
                if (player == 1)
                {
                    playerDice = 0;
                    UIManager.instance.MinusPlusButtonsEnabled(true);
                    yield return new WaitUntil(() => playerDice != 0);
                    UIManager.instance.MinusPlusButtonsEnabled(false);
                    newDice = playerDice;
                }
                else
                    newDice = squareFunction[newSquare + 1] >= squareFunction[newSquare - 1] ? 1 : -1;
                startSquare = newSquare;
                newSquare += newDice;
                yield return FigureMove(figure, startSquare, newSquare);
            }
        }
        squareState[newSquare] = player;
        if (player == 1) //Reset dices
            playerDice = 0;
        else
            rivalDice = 0;
    }

    IEnumerator FigureMove(RectTransform figure, int startSquare, int endSquare) //Move figure along path
    {
        List<Vector3> positions = new() { figure.localPosition }; //Path creation
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
        yield return new WaitForSeconds(0.5f);
    }

    void DiceChecker(int player, out int startSquare, out int endSquare) //Check correct end position
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

    void SpecialSquareChecker(int player, int oldSquare, out int newSquare) //Check for special square and effect
    {
        newSquare = oldSquare;
        if (squareFunction[oldSquare] != 0)
        {
            squareState[oldSquare] = 0;
            if (squareFunction[oldSquare] == 1) //Teleport
            {
                if (oldSquare == 0)
                    newSquare = 6;
                else if (oldSquare == 5)
                    newSquare = 12;
                UIManager.instance.SpawnActionPanel("Move to square " + (newSquare + 1), squareObjects[oldSquare].localPosition);
            }
            else if (squareFunction[oldSquare] == 2) //New dice
            {
                int dice;
                dice = Dice();
                if (player == 1)
                    playerDice = dice;
                else
                    rivalDice = dice;
                squareState[oldSquare] = player;
                DiceChecker(player, out oldSquare, out newSquare);
                if (player == 1)
                    UIManager.instance.UpdateTexts(round, turn, dice);
                else
                    UIManager.instance.SpawnActionPanel(rivalDice.ToString(), squareObjects[oldSquare].localPosition);
                UIManager.instance.SpawnActionPanel("Throw dice again", squareObjects[oldSquare].localPosition);
                squareFunction[oldSquare] = 0;
                squareObjects[oldSquare].GetComponent<RawImage>().color = Color.white;
            }
            else if (squareFunction[oldSquare] == -1) //-3 squares
            {
                newSquare = oldSquare - 3;
                UIManager.instance.SpawnActionPanel("-3 squares", squareObjects[oldSquare].localPosition);
            }
            else if (squareFunction[oldSquare] == 99) //Win
                newSquare = 99;
        }
    }

    public static int Dice() //Dice throw
    {
        return Random.Range(1, 7);
    }
}