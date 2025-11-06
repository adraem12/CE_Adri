using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI numText;
    public GameObject cannonBallPrefab;
    [HideInInspector]public List<GameObject> cannonBallList = new();
    List<CannonScript> cannons = new();

    private void Awake()
    {
        cannons = FindObjectsByType<CannonScript>(FindObjectsSortMode.None).ToList();
        Instance = this;
        numText.text = "Balas: 0";
    }

    public void Button1()
    {
        foreach (var cannon in cannons)
            cannon.Shoot(false);
        numText.text = "Balas: " + cannonBallList.Count;
    }

    public void Button2()
    {
        foreach (var ball in cannonBallList)
            Destroy(ball);
        cannonBallList.Clear();
        numText.text = "Balas: " + cannonBallList.Count;
    }

    public void Button3()
    {
        foreach (var cannon in cannons)
            cannon.Shoot(true);
        numText.text = "Balas: " + cannonBallList.Count;
    }
}