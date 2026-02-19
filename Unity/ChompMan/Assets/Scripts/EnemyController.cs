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
                ParticleSystem.MainModule particleSystem = Instantiate(GameManager.instance.destructionParticleSystem, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
                particleSystem.startColor = new Color(0, 1, 1);
                SoundManager.instance.SetEffects(1);
                Destroy(gameObject);
            }
        }
    }
}