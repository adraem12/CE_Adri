using UnityEngine;

public class KamikazeAllyState
{
    protected GameObject agent;
    protected KamikazeAllyAI allyAI;
    protected GameObject player;
    protected GameObject nearestEnemy;
    public float attackDistance, chaseDistance, explodeDistance;
    public enum STATE { PATROL, ATTACK, CHASE };
    public enum EVENT { ENTER, UPDATE, EXIT };
    public STATE name;
    protected EVENT actualFase;
    protected KamikazeAllyState nextState;

    public KamikazeAllyState(GameObject agentToSet, KamikazeAllyAI allyAI)
    {
        agent = agentToSet;
        player = GameManager.instance.player;
        this.allyAI = allyAI;
        attackDistance = allyAI.attackDistance;
        chaseDistance = allyAI.chaseDistance;
        explodeDistance = allyAI.explodeDistance;
    }

    public virtual void Entry() { actualFase = EVENT.UPDATE; }
    public virtual void Updating() { actualFase = EVENT.UPDATE; }
    public virtual void Exit() { actualFase = EVENT.EXIT; }

    public KamikazeAllyState Process()
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
        if (nearestEnemy != null && Vector3.Distance(agent.transform.position, nearestEnemy.transform.position) < explodeDistance)
            return true;
        else
            return false;
    }

    protected bool IsAtChaseDistance()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < chaseDistance)
            return true;
        else
            return false;
    }

    protected void SearchForNearestEnemy()
    {
        Collider[] hits = new Collider[10];
        int characters = Physics.OverlapSphereNonAlloc(allyAI.transform.position, attackDistance, hits, 1 << 3);
        if (characters > 0)
        {
            float distance = float.MaxValue;
            foreach (Collider c in hits)
                if (c != null && c.CompareTag("Enemy") && Vector3.Distance(c.transform.position, allyAI.transform.position) < distance)
                {
                    distance = Vector3.Distance(c.transform.position, allyAI.transform.position);
                    nearestEnemy = c.gameObject;
                }
        }
    }
}