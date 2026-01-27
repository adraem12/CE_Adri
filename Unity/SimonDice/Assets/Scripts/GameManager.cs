using System.Collections;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int[] currentColors;
    int currentItem;

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
        currentColors = new int[20];
        currentItem = 0;
        UIManager.Instance.UpdateCountText(currentItem);
    }

    public void ButtonCheck(int i)
    {
        currentColors.SetValue(i, currentItem);
        currentItem++;
        UIManager.Instance.UpdateCountText(currentItem);
        if (currentItem >= currentColors.Length)
            foreach (Button button in UIManager.Instance.colorButtons)
                button.interactable = false;
    }

    public void SettingButton()
    {
        StartCoroutine(ShowCount());
    }

    IEnumerator ShowCount()
    {
        foreach (Button button in UIManager.Instance.colorButtons)
            button.interactable = false;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < currentItem; i++) 
        {
            UIManager.Instance.colorButtons[currentColors[i]].GetComponent<Animator>().SetTrigger("Activate");
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        foreach (Button button in UIManager.Instance.colorButtons)
            button.interactable = true;
    }

    public void ShowFirstOrLast(bool first)
    {
        if (currentItem != 0 && first)
            Debug.Log(currentColors.First());
        else if (!first)
            Debug.Log(currentColors.Last());
        else
            Debug.Log("_");
    }
}