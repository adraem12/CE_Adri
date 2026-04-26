using UnityEngine;
using UnityEngine.AI;

public class ShooterPatrol : ShooterState
{
    public ShooterPatrol(GameObject newAgentToSet, ShooterEnemyAI newEnemyAI) : base(newAgentToSet, newEnemyAI)
    {
        name = STATE.PATROL;
    }

    public override void Entry()
    {
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.darkOliveGreen;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public override void Updating()
    {
        if (IsAtChaseDistance())
        {
            nextState = new ShooterChase(agent, enemyAI);
            actualFase = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}