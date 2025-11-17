using System.Collections;
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
    [HideInInspector] public float currentForce = 0;
    MeshRenderer meshRenderer;
    float materialTimer = 0;

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
        cannonBody.LookAt(cross.position + Vector3.up * 2f); // Apunta hacia la mirilla
        if (materialTimer != 0) // Contador del material
        {
            materialTimer -= Time.deltaTime;
            if (materialTimer < 0)
            {
                meshRenderer.material = whiteMaterial;
                materialTimer = 0;
            }
        }
    }

    public void Shoot()
    {
        Rigidbody newBall = Instantiate(GameManager.Instance.cannonBallPrefab, cannonTip.position, Quaternion.identity).GetComponent<Rigidbody>();
        newBall.AddForce(cannonForce * currentForce * cannonTip.up.normalized, ForceMode.Impulse); // Impulsa la bola en la dirección del cañón
        materialTimer = 0.15f; // Resetea los valores tras disparar
        meshRenderer.material = redMaterial;
    }

    public IEnumerator ChargeCannon()
    {
        currentForce = 0;
        while (currentForce < 3) // Carga la fuerza hasta llegar al máximo
        {
            yield return new WaitForFixedUpdate();
            currentForce += Time.deltaTime * 1.5f;
        }
        currentForce = 3;
    }
}