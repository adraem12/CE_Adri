using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public enum handType
{
    none, rock, paper, scissors, lizard, spok
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float turnsToWin = 3;

    private void Awake()
    {
        instance = this;
    }

    public void StartNewGame()
    {
        
        StartCoroutine(TurnSystem());
    }

    IEnumerator TurnSystem()
    {
        float playerTurns = 0;
        float rivalTurns = 0;
        handType playerCurrentHand = handType.none;
        handType rivalCurrentHand = handType.none;
        while (playerTurns < turnsToWin || rivalTurns < turnsToWin) 
        {
            Debug.Log("Elige...");
            UIManager.instance.ActivatePlayerHand();
            while (playerCurrentHand == handType.none)
                yield return null;
            RivalTurn(out rivalCurrentHand);
            CompareHands(playerCurrentHand, rivalCurrentHand);
        }
        yield return null;
    }

    void RivalTurn(out handType hand)
    {
        hand = (handType)UnityEngine.Random.Range(0, (float)Enum.GetValues(typeof(handType)).Cast<handType>().Max());
    }

    bool CompareHands(handType playerHand, handType rivalHand)
    {
        return false;
    }
}