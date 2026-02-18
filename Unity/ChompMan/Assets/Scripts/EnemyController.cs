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
        if (!GameManager.cherryState) //Normal behaviour
            agent.destination = player.transform.position;
        if (agent.isOnOffMeshLink && agent.currentOffMeshLinkData.startPos.x != 0) //TP behaviour
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
            if (!GameManager.cherryState) //Kill player
                GameManager.instance.GameOver(false);
            else //Get killed by player
            {
                GameManager.enemiesLeft--;
                GameManager.enemiesKilled++;
                UIManager.Instance.UpdateEnemiesText();
                Destroy(gameObject);
                SoundManager.instance.SetEffects(1);
            }
        }
    }
}