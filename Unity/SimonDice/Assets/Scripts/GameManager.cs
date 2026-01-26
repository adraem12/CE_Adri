using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<int> completeList, currentPlayerList;
    [HideInInspector] public int currentRound;
    public static int maxRound = 0;
    bool playing = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        completeList = new List<int>();
        currentPlayerList = new List<int>();
        currentRound = 0;
        StartCoroutine(Game());
    }

    IEnumerator Game()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            AddColorToList();
            foreach (int i in completeList)
            {
                UIManager.Instance.ReproduceColor(i);
                yield return new WaitForSeconds(1f);
            }
            playing = true;
            UIManager.Instance.ActivateButtons(true);
            currentPlayerList.Clear();
            yield return new WaitUntil(() => !playing);
        }
    }

    void AddColorToList()
    {
        completeList.Add(Random.Range(0, 4));
    }

    public void ButtonCheck(int i)
    {
        currentPlayerList.Add(i);
        if (currentPlayerList.IndexOf(i) != completeList.IndexOf(i))
        {
            StopAllCoroutines();
            UIManager.Instance.GameEnd();
            return;
        }
        if (currentPlayerList.Count == completeList.Count)
        {
            playing = false;
            currentRound++;
            UIManager.Instance.currentRoundText.text = "Current round: " + currentRound;
            UIManager.Instance.ActivateButtons(false);
        }
    }
}