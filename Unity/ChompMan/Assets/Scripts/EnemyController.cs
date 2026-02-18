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
        if (!GameManager.cherryState)
            agent.destination = player.transform.position;
        if (agent.isOnOffMeshLink && agent.currentOffMeshLinkData.startPos.x != 0)
        {
            if (Vector3.Distance(transform.position, agent.currentOffMeshLinkData.startPos) > Vector3.Distance(transform.position, agent.currentOffMeshLinkData.endPos))
                transform.position = agent.currentOffMeshLinkData.startPos;
            else
                transform.position = agent.currentOffMeshLinkData.endPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            if (!GameManager.cherryState)
                GameManager.instance.GameOver(false);
            else
            {
                GameManager.enemiesLeft--;
                GameManager.enemiesKilled++;
                Destroy(gameObject);
            }
        }
    }
}