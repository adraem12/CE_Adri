using UnityEngine;
using UnityEngine.AI;

public class KamikazeAllyAttack : KamikazeAllyState
{
    public KamikazeAllyAttack(GameObject newAgentToSet, KamikazeAllyAI newAllyAI) : base(newAgentToSet, newAllyAI)
    {
        name = STATE.ATTACK;
    }

    public override void Entry()
    {
        base.Entry();
        agent.transform.localScale = Vector3.one * 2f;
        agent.transform.position += Vector3.up * 0.5f;
        agent.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Updating()
    {
        SearchForNearestEnemy();
        if (nearestEnemy == null) 
        {
            nextState = new KamikazeAllyChase(agent, allyAI);
            agent.transform.localScale = Vector3.one;
            agent.transform.position -= Vector3.up * 0.5f;
            agent.GetComponent<NavMeshAgent>().speed *= 0.5f;
            actualFase = EVENT.EXIT;
        }
        else
        {
            agent.GetComponent<NavMeshAgent>().SetDestination(nearestEnemy.transform.position);
            agent.transform.LookAt(nearestEnemy.transform.position);
            if (IsAtAttackDistance())
                agent.GetComponent<KamikazeAllyAI>().Attack(nearestEnemy);
        }
    }
}