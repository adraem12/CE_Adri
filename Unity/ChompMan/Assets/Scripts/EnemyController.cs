using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > agent.radius + 1f)
            agent.destination = player.transform.position;
        else
            agent.destination = transform.position;
    }
}