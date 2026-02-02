using System.Collections;
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
        if (currentItem < currentColors.Length)
        {
            currentColors.SetValue(i, currentItem);
            currentItem++;
            UIManager.Instance.UpdateCountText(currentItem);
            UIManager.Instance.UpdateColorText(i);
        }
        else
            UIManager.Instance.UpdateColorText(-1);
    }

    public void SettingButton()
    {
        StartCoroutine(ShowSet());
    }

    IEnumerator ShowSet()
    {
        UIManager.Instance.settingButton.enabled = false;
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
        StartNewGame();
        UIManager.Instance.settingButton.enabled = true;
        StopAllCoroutines();
    }

    public void ShowFirstOrLast(bool first)
    {
        if (currentItem != 0 && first)
            UIManager.Instance.UpdateColorText(currentColors.First());
        else if (currentItem != 0 && !first)
            UIManager.Instance.UpdateColorText(currentColors[currentItem - 1]);
    }

    public void ShowColorCount()
    {
        int g = 0, r = 0, b = 0, y = 0;
        for (int i = 0; i < currentItem; i++)
            switch (currentColors[i])
            {
                case 0:
                    g++;
                    break;
                case 1:
                    r++;
                    break;
                case 2:
                    b++;
                    break;
                case 3:
                    y++;
                    break;
            }
        Debug.Log("You clicked the green button " + g + " times, the red button " + r + " times, the blue button " + b + " times and the yellow button " + y + " times");
    }
}