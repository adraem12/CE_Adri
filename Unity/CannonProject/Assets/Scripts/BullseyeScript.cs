using UnityEngine;

public class BullseyeScript : MonoBehaviour
{
    // Variables
    public float timer;
    public GameObject destroyedBullseye;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            DeadBullseye(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CannonBall")) // Recibe impacto de bala
        {
            Destroy(other.gameObject);
            DeadBullseye(true);
        }
    }

    void DeadBullseye(bool hit)
    {
        GameManager.Instance.SpawnBullseye(hit, transform.position);
        if (hit)
            GameManager.Instance.audioSources[1].Play();
        Destroy(gameObject);
        Instantiate(destroyedBullseye, transform.position, Quaternion.identity);
    }
}