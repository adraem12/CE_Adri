using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI numText;
    public Transform cannonTip;
    public float cannonForce;
    public GameObject cannonBallPrefab;
    List<GameObject> cannonBallList = new();
    List<Color> cannonBallColors = new() { Color.red, Color.blue, Color.green, Color.black, Color.white };

    private void Awake()
    {
        Instance = this;
        numText.text = "Balas: 0";
    }

    public void Button1()
    {
        Rigidbody newBall = Instantiate(cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        newBall.AddForce(cannonTip.up.normalized * cannonForce, ForceMode.Impulse);
        cannonBallList.Add(newBall.gameObject);
        numText.text = "Balas: " + cannonBallList.Count;
    }

    public void Button2()
    {
        foreach (var ball in cannonBallList)
            Destroy(ball);
        cannonBallList.Clear();
        numText.text = "Balas: 0";
    }

    public void Button3()
    {
        Rigidbody newBall = Instantiate(cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        newBall.transform.localScale = Vector3.one * Random.Range(0.1f, 2.1f);
        newBall.gameObject.GetComponent<MeshRenderer>().material.color = cannonBallColors[Random.Range(0, 5)];
        newBall.AddForce(cannonForce * Random.Range(0.1f, 2.1f) * cannonTip.up.normalized, ForceMode.Impulse);
        cannonBallList.Add(newBall.gameObject);
        numText.text = "Balas: " + cannonBallList.Count;
    }
}