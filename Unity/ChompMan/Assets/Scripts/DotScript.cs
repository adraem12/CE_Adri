using UnityEngine;

public class DotScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.dotsLeft--;
            UIManager.Instance.UpdateDotsText();
            Destroy(gameObject);
        }
    }
}