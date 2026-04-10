using UnityEngine;
using UnityEngine.AI;

public class KamikazeAllyPatrol : KamikazeAllyState
{
    public KamikazeAllyPatrol(GameObject newAgentToSet, KamikazeAllyAI newAllyAI) : base(newAgentToSet, newAllyAI)
    {
        name = STATE.PATROL;
    }

    public override void Entry()
    {
        base.Entry();
        agent.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public override void Updating()
    {
        if (IsAtChaseDistance())
        {
            nextState = new KamikazeAllyChase(agent, allyAI);
            actualFase = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}