using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int[] squareState;
    int[] squareFunction;
    public RectTransform[] squareObjects;
    public RectTransform playerFigure, aiFigure;
    int round = 1;
    int turn = 0;
    public int dice = 0;

    private void Awake()
    {
        instance = this;
        squareState = new int[21];
        squareFunction = new int[21];
        for (int i = 0; i < squareState.Length; i++)
            squareState[i] = 0;
        for (int i = 0; i < squareFunction.Length; i++)
            squareFunction[i] = 0;
        squareState[0] = -1;
        //Teleports
        squareFunction[0] = 1;
        squareFunction[5] = 1;
        //Roll again
        squareFunction[12] = 2;
        squareFunction[17] = 2;
        //Move back 3 squares
        squareFunction[4] = -1;
        squareFunction[9] = -1;
        squareFunction[13] = -1;
        squareFunction[18] = -1;
        squareFunction[19] = -1;
        //Win
        squareFunction[20] = 99;
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
            yield return new WaitUntil(() => dice != 0);
            yield return new WaitForSeconds(1);
            if (squareState[0] == -1)
            {
                squareState[0] = 2;
                playerFigure.localPosition = squareObjects[dice].localPosition;
                squareState[dice] = 1;
            }
            else
                for (int i = 0; i < squareState.Length; i++)
                    if (squareState[i] == 1)
                    {
                        squareState[i] = 0;
                        if (i + dice < squareState.Length)
                        {
                            squareState[i + dice] = 1;
                            playerFigure.localPosition = squareObjects[i + dice].localPosition;
                        }
                        break;
                    }
            dice = 0;
            UIManager.instance.diceButton.interactable = true;
        }
    }

    public static int Dice()
    {
        return Random.Range(1, 7);
    }
}