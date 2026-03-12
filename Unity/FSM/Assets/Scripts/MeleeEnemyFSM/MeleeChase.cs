using UnityEngine;
using UnityEngine.AI;

public class MeleeChase : MeleeState
{
    public MeleeChase(GameObject newAgentToSet, MeleeEnemyAI newEnemyAI) : base(newAgentToSet, newEnemyAI)
    {
        name = STATE.CHASE; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.yellow;
        agent.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Updating()
    {
        agent.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        if (IsAtAttackDistance())
        {
            nextState = new MeleeAttack(agent, enemyAI); // Si el NPC puede atacar al jugador, lo ponemos a atacar.
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de PERSEGUIR a ATACAR.
        }
        else if (!IsAtChaseDistance())
        {
            nextState = new MeleePatrol(agent, enemyAI); // Si el NPC no puede persegur al jugador, lo ponemos a vigilar.
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de PERSEGUIR a PATRULLAR.
        }
    }

    public override void Exit()
    {
        // Le resetearíamos la animación de disparar, o lo que sea...
        base.Exit();
    }
}