using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public enum HandType
{
    None, Rock, Paper, Scissors, Lizard, Spok
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int roundsToWin = 3;
    int currentRound;
    [HideInInspector] public HandType playerCurrentHand = HandType.None;
    HandType rivalCurrentHand = HandType.None;

    private void Awake()
    {
        instance = this;
    }

    public void StartNewGame()
    {
        currentRound = 1;
        UIManager.instance.SetRoundText(0, 0, currentRound);
        StartCoroutine(TurnSystem());
    }

    IEnumerator TurnSystem()
    {
        int playerRounds = 0;
        int rivalRounds = 0;
        playerCurrentHand = HandType.None;
        rivalCurrentHand = HandType.None;
        while (playerRounds < roundsToWin && rivalRounds < roundsToWin) 
        {
            UIManager.instance.SetInfoText("Choose your play...");
            UIManager.instance.ActiveActionButton(true);
            yield return new WaitUntil(() => playerCurrentHand != HandType.None);
            UIManager.instance.ActiveActionButton(false);
            RivalTurn(out rivalCurrentHand);
            UIManager.instance.ShowHands(playerCurrentHand, rivalCurrentHand, true);
            if (CompareHands(playerCurrentHand, rivalCurrentHand) == 1)
            {
                UIManager.instance.SetInfoText("You win this round");
                playerRounds++;
            }
            else if (CompareHands(playerCurrentHand, rivalCurrentHand) == -1)
            {
                UIManager.instance.SetInfoText("Your rival win this round");
                rivalRounds++;
            }
            else
                UIManager.instance.SetInfoText("It's a tie");
            yield return new WaitForSeconds(3.5f);
            currentRound++;
            UIManager.instance.ShowHands(playerCurrentHand, rivalCurrentHand, false);
            UIManager.instance.SetRoundText(playerRounds, rivalRounds, currentRound);
            playerCurrentHand = HandType.None;
            rivalCurrentHand = HandType.None;
        }
        if (playerRounds > rivalRounds)
            UIManager.instance.GameEnd(true);
        else
            UIManager.instance.GameEnd(false);
        StopAllCoroutines();
    }

    void RivalTurn(out HandType hand)
    {
        hand = (HandType)UnityEngine.Random.Range(1, (float)Enum.GetValues(typeof(HandType)).Cast<HandType>().Max());
    }

    int CompareHands(HandType playerHand, HandType rivalHand)
    {
        switch (playerHand) 
        {
            case HandType.Rock:
                switch (rivalHand)
                {
                    case HandType.Rock:
                        return 0;
                    case HandType.Paper:
                        return -1;
                    case HandType.Scissors:
                        return 1;
                    case HandType.Lizard:
                        return 1;
                    case HandType.Spok:
                        return -1;
                }
                break;
            case HandType.Paper:
                {
                    switch (rivalHand)
                    {
                        case HandType.Rock:
                            return 1;
                        case HandType.Paper:
                            return 0;
                        case HandType.Scissors:
                            return -1;
                        case HandType.Lizard:
                            return -1;
                        case HandType.Spok:
                            return 1;
                    }
                }
                break;
            case HandType.Scissors:
                {
                    switch (rivalHand)
                    {
                        case HandType.Rock:
                            return -1;
                        case HandType.Paper:
                            return 1;
                        case HandType.Scissors:
                            return 0;
                        case HandType.Lizard:
                            return 1;
                        case HandType.Spok:
                            return -1;
                    }
                }
                break;
            case HandType.Lizard:
                {
                    switch (rivalHand)
                    {
                        case HandType.Rock:
                            return -1;
                        case HandType.Paper:
                            return 1;
                        case HandType.Scissors:
                            return -1;
                        case HandType.Lizard:
                            return 0;
                        case HandType.Spok:
                            return 1;
                    }
                }
                break;
            case HandType.Spok:
                {
                    switch (rivalHand)
                    {
                        case HandType.Rock:
                            return 1;
                        case HandType.Paper:
                            return -1;
                        case HandType.Scissors:
                            return 1;
                        case HandType.Lizard:
                            return -1;
                        case HandType.Spok:
                            return 0;
                    }
                }
                break;
        }
        return 0;
    }
}