using UnityEngine;

public class DotScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) //Eat dot
        {
            GameManager.dotsLeft--;
            UIManager.Instance.UpdateDotsText();
            Destroy(gameObject);
            SoundManager.instance.SetEffects(0);
        }
    }
}