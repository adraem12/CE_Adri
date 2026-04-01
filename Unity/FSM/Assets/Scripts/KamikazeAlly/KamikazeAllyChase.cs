using UnityEngine;
using UnityEngine.AI;

public class KamikazeAllyChase : KamikazeAllyState
{
    public KamikazeAllyChase(GameObject newAgentToSet, KamikazeAllyAI newAllyAI) : base(newAgentToSet, newAllyAI)
    {
        name = STATE.CHASE; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.yellowGreen;
        agent.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Updating()
    {
        SearchForNearestEnemy();
        agent.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        agent.GetComponent<NavMeshAgent>().isStopped = Vector3.Distance(agent.transform.position, player.transform.position) < 1f;
        if (IsAtAttackDistance())
        {
            nextState = new KamikazeAllyAttack(agent, allyAI); // Si el NPC puede atacar al jugador, lo ponemos a atacar.
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de PERSEGUIR a ATACAR.
        }
        /*
        else if (!IsAtChaseDistance())
        {
            nextState = new KamikazeAllyPatrol(agent, allyAI); // Si el NPC no puede persegur al jugador, lo ponemos a vigilar.
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de PERSEGUIR a PATRULLAR.
        }
        */
    }

    public override void Exit()
    {
        // Le resetearíamos la animación de disparar, o lo que sea...
        base.Exit();
    }
}
