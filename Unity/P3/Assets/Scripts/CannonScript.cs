using System;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    //Variables
    Transform cannonBody;
    Transform cannonTip;
    Transform cross;
    public Material whiteMaterial;
    public Material redMaterial;
    public float cannonForce;
    MeshRenderer meshRenderer;
    readonly List<Color> cannonBallColors = new() { Color.red, Color.blue, Color.green, Color.black, Color.white };
    Collider[] nearBalls;

    private void Awake()
    {
        cannonBody = transform.GetChild(0);
        cannonTip = cannonBody.GetChild(0);
        meshRenderer = cannonBody.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        cross = GameManager.Instance.cross;
    }

    private void Update()
    {
        transform.LookAt(cross.position + Vector3.up * 2f); // Apunta hacia la mirilla
        nearBalls = new Collider[1];
        // Busca si hay alguna bala cerca
        Physics.OverlapSphereNonAlloc(cannonTip.position + cannonTip.up * 1f, 1.2f, nearBalls, LayerMask.GetMask("CannonBall"));
        if (nearBalls[0] != null)
            meshRenderer.material = redMaterial;
        else
            meshRenderer.material = whiteMaterial;
    }

    public void Shoot(bool random)
    {
        float mult = random ? UnityEngine.Random.Range(0.1f, 2.1f) : 1f; // Randomiza o no la fuerza
        Rigidbody newBall = Instantiate(GameManager.Instance.cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        if (random) // Randomiza la escala y el color
        {
            newBall.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.1f, 2.1f);
            newBall.gameObject.GetComponent<MeshRenderer>().material.color = cannonBallColors[UnityEngine.Random.Range(0, 5)];
        }
        newBall.AddForce(cannonForce * mult * cannonTip.up.normalized, ForceMode.Impulse); // Impulsa la bola en la dirección del cañón
        GameManager.Instance.cannonBallList.Add(newBall.gameObject);
    }
}