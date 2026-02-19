using UnityEngine;

public class CherryScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) //Eat Cherry
        {
            GameManager.instance.StopAllCoroutines();
            GameManager.instance.StartCoroutine(GameManager.instance.CherryState());
            ParticleSystem.MainModule particleSystem = Instantiate(GameManager.instance.destructionParticleSystem, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
            particleSystem.startColor = new Color(1, 0, 0);
            Destroy(gameObject);
        }
    }
}