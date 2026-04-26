using System;
using UnityEngine;

public class ShooterState
{
    protected GameObject agent;
    protected ShooterEnemyAI enemyAI;
    protected GameObject player;
    public float attackDistance, chaseDistance;
    RaycastHit[] visionHits;
    Ray visionray = new();

    public enum STATE { PATROL, ATTACK, CHASE };

    public enum EVENT { ENTER, UPDATE, EXIT };
    public STATE name;
    protected EVENT actualFase;
    protected ShooterState nextState;

    public ShooterState(GameObject agentToSet, ShooterEnemyAI enemyAI) 
    { 
        agent = agentToSet;
        player = GameManager.instance.player;
        this.enemyAI = enemyAI;
        attackDistance = enemyAI.attackDistance;
        chaseDistance = enemyAI.chaseDistance;
    }

    public virtual void Entry() { actualFase = EVENT.UPDATE; }
    public virtual void Updating() { actualFase = EVENT.UPDATE; }
    public virtual void Exit() { actualFase = EVENT.EXIT; }

    public ShooterState Process()
    {
        if (actualFase == EVENT.ENTER) 
            Entry();
        if (actualFase == EVENT.UPDATE) 
            Updating();
        if (actualFase == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    protected bool IsAtAttackDistance()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < attackDistance)
            return true;
        else
            return false;
    }
    protected bool IsAtChaseDistance()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) >= chaseDistance)
            return false;
        else
            return true;
    }

    protected bool CanSeePlayer()
    {
        visionHits = new RaycastHit[5];
        visionray = new(agent.transform.position + agent.transform.forward * 0.51f, player.transform.position - agent.transform.position);
        int colls = Physics.RaycastNonAlloc(visionray, visionHits, (player.transform.position - agent.transform.position).magnitude);
        if (colls > 0)
        {
            Array.Sort(visionHits, (a, b) => a.distance.CompareTo(b.distance));
            for (int i = 0; i < visionHits.Length; i++)
            {
                if (visionHits[i].distance == 0)
                    continue;
                if (visionHits[i].collider != null && visionHits[i].collider.GetComponent<BulletScript>())
                    continue;
                if (visionHits[i].collider != null && visionHits[i].collider.CompareTag("Player"))
                    return true;
                else if (visionHits[i].collider != null && !visionHits[i].collider.CompareTag("Player"))
                    return false;
            }
        }
        return false;
    }
}