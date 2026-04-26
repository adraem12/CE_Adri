using UnityEngine;
using UnityEngine.AI;

public class ShooterChase : ShooterState
{
    public ShooterChase(GameObject newAgentToSet, ShooterEnemyAI newEnemyAI) : base(newAgentToSet, newEnemyAI)
    {
        name = STATE.CHASE;
    }

    public override void Entry()
    {
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.yellow;
        agent.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Updating()
    {
        agent.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        if (IsAtAttackDistance() && CanSeePlayer())
        {
            nextState = new ShooterAttack(agent, enemyAI);
            actualFase = EVENT.EXIT;
        }
        else if (!IsAtChaseDistance())
        {
            nextState = new ShooterPatrol(agent, enemyAI);
            actualFase = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}