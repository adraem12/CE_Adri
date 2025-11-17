using System;
using UnityEngine;

public class BullseyeScript : MonoBehaviour
{
    // Variables
    public float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CannonBall")) // Recibe impacto de bala
        {
            Destroy(other.gameObject);
            GameManager.Instance.SpawnBullseye(Convert.ToInt32(transform.parent.name));
            Destroy(gameObject);
        }
    }
}