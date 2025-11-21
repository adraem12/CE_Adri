using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // Variables
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI forceText;
    public Slider forceSlider;
    CannonScript cannon;

    private void Awake()
    {
        Instance = this;
        shotsText.text = "Shots: 0";
        hitText.text = "Hits: 0";
    }

    void Start()
    {
        cannon = GameManager.Instance.cannon;
        forceSlider.maxValue = cannon.maxForce;
    }

    void Update()
    {
        forceSlider.value = cannon.currentForce; // Actualiza la barra de disparo
        forceText.text = Mathf.Floor(cannon.currentForce).ToString();
    }

    public void ShotUI(int i)
    {
        shotsText.text = "Shots: " + i;
    }

    public void HitUI(int i)
    {
        hitText.text = "Hits: " + i;
    }
}