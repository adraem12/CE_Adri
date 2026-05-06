using UnityEngine;
using UnityEngine.AI;

public class ShooterAttack : ShooterState
{
    Vector3 lastplayerPosition;
    bool shooting;

    public ShooterAttack(GameObject newAgentToSet, ShooterEnemyAI newEnemyAI) : base(newAgentToSet, newEnemyAI)
    {
        name = STATE.ATTACK;
    }

    public override void Entry()
    {
        base.Entry();
        lastplayerPosition = player.transform.position;
        agent.GetComponent<Renderer>().material.color = Color.indianRed;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
        shooting = true;
        agent.GetComponent<ShooterEnemyAI>().StartAttacking();
    }

    public override void Updating()
    {
        agent.transform.LookAt(lastplayerPosition);
        agent.GetComponent<NavMeshAgent>().SetDestination(lastplayerPosition);
        if (!CanSeePlayer())
        {
            if (shooting)
                SetFindPlayer();
            if (agent.transform.position == lastplayerPosition)
            {
                nextState = new ShooterChase(agent, enemyAI);
                actualFase = EVENT.EXIT;
            }
        }
        else
        {
            lastplayerPosition = player.transform.position;
            if (shooting && !IsAtAttackDistance())
                SetFindPlayer();
            if (!shooting && IsAtAttackDistance())
                SetAttackPlayer();
        }
    }

    void SetFindPlayer()
    {
        shooting = false;
        agent.GetComponent<NavMeshAgent>().isStopped = false;
        agent.GetComponent<ShooterEnemyAI>().StopAttacking();
    }

    void SetAttackPlayer()
    {
        shooting = true;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
        agent.GetComponent<ShooterEnemyAI>().StartAttacking();
    }

    public override void Exit()
    {
        agent.GetComponent<ShooterEnemyAI>().StopAttacking();
        base.Exit();
    }
}