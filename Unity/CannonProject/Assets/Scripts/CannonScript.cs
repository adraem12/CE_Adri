using System;
using System.Collections;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    //Variables
    public Transform cannonBodyV;
    public Transform cannonTip;
    Transform cross;
    public Material baseMaterial;
    public Material redMaterial;
    public float cannonForce;
    [HideInInspector] public float currentForce = 0;
    [HideInInspector] public int maxForce = 100;
    public MeshRenderer[] meshRenderers;
    float materialTimer = 0;

    private void Start()
    {
        cross = GameManager.Instance.cross.transform;
    }

    private void Update()
    {
        cannonBodyV.LookAt(cross.position + Vector3.up * 2f); // Apunta hacia la mirilla en vertical
        transform.LookAt(new Vector3(cross.position.x, 0, 0)); // Apunta hacia la mirilla en vertical
        if (materialTimer != 0) // Contador del material
        {
            materialTimer -= Time.deltaTime;
            if (materialTimer < 0)
            {
                foreach (MeshRenderer mr in meshRenderers)
                    mr.material = baseMaterial;
                materialTimer = 0;
            }
        }
    }

    public void Shoot()
    {
        Rigidbody newBall = Instantiate(GameManager.Instance.cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        newBall.AddForce(cannonForce * (currentForce / 35) * cannonTip.up.normalized, ForceMode.Impulse); // Impulsa la bola en la dirección del cañón
        materialTimer = 0.15f; // Resetea los valores tras disparar
        foreach (MeshRenderer mr in meshRenderers)
            mr.material = redMaterial;
    }

    public IEnumerator ChargeCannon()
    {
        currentForce = 0;
        while (currentForce < maxForce) // Carga la fuerza hasta llegar al máximo
        {
            yield return new WaitForFixedUpdate();
            currentForce += Time.deltaTime * 75f;
        }
        currentForce = maxForce;
    }
}