using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button[] colorButtons;
    public Button settingButton;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI colorText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCountText(int i)
    {
        countText.text = "Count: " + i + "/20";
    }

    public void UpdateColorText(int i)
    {
        StopAllCoroutines();
        switch (i) 
        {
            case 0:
                StartCoroutine(TextTimer("GREEN", Color.green));
                break;
            case 1:
                StartCoroutine(TextTimer("RED", Color.red));
                break;
            case 2:
                StartCoroutine(TextTimer("BLUE", Color.blue));
                break;
            case 3:
                StartCoroutine(TextTimer("YELLOW", Color.yellow));
                break;
            case -1:
                StartCoroutine(TextTimer("MAXIMUM COLOURS ACHIEVED", Color.white));
                break;
        }
    }

    IEnumerator TextTimer(string colorName, Color color)
    {
        colorText.text = colorName;
        colorText.color = color;
        yield return new WaitForSeconds(3);
        colorText.text = "";
    }

    public void ReplayButton()
    {
        GameManager.Instance.StartNewGame();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}