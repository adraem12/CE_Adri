using System;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Transform cannonTip;
    public Material whiteMaterial;
    public Material redMaterial;
    public float cannonForce;
    MeshRenderer meshRenderer;
    readonly List<Color> cannonBallColors = new() { Color.red, Color.blue, Color.green, Color.black, Color.white };
    Collider[] nearBalls;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        nearBalls = new Collider[2];
        Physics.OverlapSphereNonAlloc(cannonTip.position + cannonTip.up * 0.8f, 1f, nearBalls);
        if (nearBalls[1] != null)
            meshRenderer.material = redMaterial;
        else
            meshRenderer.material = whiteMaterial;
    }

    public void Shoot(bool random)
    {
        float mult = random ? UnityEngine.Random.Range(0.1f, 2.1f) : 1f;
        Rigidbody newBall = Instantiate(GameManager.Instance.cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        if (random)
        {
            newBall.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.1f, 2.1f);
            newBall.gameObject.GetComponent<MeshRenderer>().material.color = cannonBallColors[UnityEngine.Random.Range(0, 5)];
        }
        newBall.AddForce(cannonForce * mult * cannonTip.up.normalized, ForceMode.Impulse);
        GameManager.Instance.cannonBallList.Add(newBall.gameObject);
    }
}