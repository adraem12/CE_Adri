using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public GameObject destroyedBall;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(destroyedBall, transform.position, Quaternion.identity);
    }
}