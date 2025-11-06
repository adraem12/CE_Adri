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

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            meshRenderer.material = redMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
            meshRenderer.material = whiteMaterial;
    }

    public void Shoot(bool random)
    {
        float mult = random ? Random.Range(0.1f, 2.1f) : 1f;
        Rigidbody newBall = Instantiate(GameManager.Instance.cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        if (random)
        {
            newBall.transform.localScale = Vector3.one * Random.Range(0.1f, 2.1f);
            newBall.gameObject.GetComponent<MeshRenderer>().material.color = cannonBallColors[Random.Range(0, 5)];
        }
        newBall.AddForce(cannonForce * mult * cannonTip.up.normalized, ForceMode.Impulse);
        GameManager.Instance.cannonBallList.Add(newBall.gameObject);
    }
}