using UnityEngine;

public class DotScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) //Eat dot
        {
            GameManager.dotsLeft--;
            UIManager.Instance.UpdateDotsText();
            SoundManager.instance.SetEffects(0);
            Instantiate(GameManager.instance.destructionParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}