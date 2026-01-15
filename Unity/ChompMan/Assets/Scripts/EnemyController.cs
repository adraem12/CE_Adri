using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.instance.player;
    }

    void Update()
    {
        agent.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
            GameManager.instance.GameOver();
    }
}