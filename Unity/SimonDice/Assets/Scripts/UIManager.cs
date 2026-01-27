using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button[] colorButtons;
    public GameObject replayPanel;
    public TextMeshProUGUI countText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCountText(int i)
    {
        countText.text = "Count: " + i + "/20";
    }

    // Buttons
    public void ReplayButton()
    {
        GameManager.Instance.StartNewGame();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}