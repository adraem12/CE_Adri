using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(GameObject newAgentToSet) : base(newAgentToSet)
    {
        name = STATE.ATTACK; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.red;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
        agent.GetComponent<EnemyAI>().StartAttacking();
    }

    public override void Updating()
    {
        if (!IsAtAttackDistance())
        {
            nextState = new Chase(agent); // Si el NPC no puede atacar al jugador, lo ponemos a perseguir.
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de ATACAR a PERSEGUIR.
        }
    }

    public override void Exit()
    {
        // Le resetearíamos la animación de disparar, o lo que sea...
        agent.GetComponent<EnemyAI>().StopAttacking();
        base.Exit();
    }
}