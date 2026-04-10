using UnityEngine;
using UnityEngine.AI;

public class KamikazeAllyChase : KamikazeAllyState
{
    public KamikazeAllyChase(GameObject newAgentToSet, KamikazeAllyAI newAllyAI) : base(newAgentToSet, newAllyAI)
    {
        name = STATE.CHASE;
    }

    public override void Entry()
    {
        base.Entry();
        agent.transform.localScale = Vector3.one;
        agent.transform.position += Vector3.up * 0.25f;
        agent.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Updating()
    {
        SearchForNearestEnemy();
        agent.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        agent.GetComponent<NavMeshAgent>().isStopped = Vector3.Distance(agent.transform.position, player.transform.position) < explodeDistance;
        if (nearestEnemy != null)
        {
            nextState = new KamikazeAllyAttack(agent, allyAI);
            agent.GetComponent<NavMeshAgent>().speed *= 2f;
            actualFase = EVENT.EXIT;
        }
        else if (!IsAtChaseDistance())
        {
            nextState = new KamikazeAllyPatrol(agent, allyAI);
            agent.transform.localScale = Vector3.one * 0.5f;
            agent.transform.position -= Vector3.up * 0.25f;
            actualFase = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}