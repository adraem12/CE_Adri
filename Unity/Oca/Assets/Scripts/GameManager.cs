using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int[] squareState;
    int[] squareFunction;
    GameObject[] squareObjects;

    private void Awake()
    {
        instance = this;
        squareState = new int[21];
        squareFunction = new int[21];
        squareObjects = new GameObject[21];
        for (int i = 0; i < squareState.Length; i++)
            squareState[i] = 0;
        for (int i = 0; i < squareFunction.Length; i++)
            squareFunction[i] = 0;
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
        //Find squares
        for (int i = 0; i < squareObjects.Length; i++)
            squareObjects[i] = GameObject.Find("Square" + i);
    }

    public static void Dice(out int a)
    {
        a = Random.Range(0, 7);
    }
}