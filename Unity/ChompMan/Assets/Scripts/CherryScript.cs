using UnityEngine;

public class CherryScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) //Eat Cherry
        {
            GameManager.instance.StopAllCoroutines();
            GameManager.instance.StartCoroutine(GameManager.instance.CherryState());
            Destroy(gameObject);
        }
    }
}