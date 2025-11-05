using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Material whiteMaterial;
    public Material redMaterial;
    MeshRenderer meshRenderer;

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
}