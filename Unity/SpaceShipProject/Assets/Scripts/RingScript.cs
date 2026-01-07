using UnityEngine;

public class RingScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ship>())
        {
            GameManager.Instance.AddRing();
            RingManager.Instance.NextRing();
        }
    }
}