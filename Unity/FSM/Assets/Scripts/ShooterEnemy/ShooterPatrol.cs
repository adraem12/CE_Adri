using UnityEngine;
using UnityEngine.AI;

public class ShooterPatrol : ShooterState
{
    public ShooterPatrol(GameObject newAgentToSet, ShooterEnemyAI newEnemyAI) : base(newAgentToSet, newEnemyAI)
    {
        name = STATE.PATROL; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondriamos la animación de andar, calcular los puntos por los que patrulla, etc...
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.green;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public override void Updating()
    {
        if (IsAtChaseDistance()) // Le decimos que se vaya moviendo y patrullando...
        {
            nextState = new ShooterChase(agent, enemyAI);
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de VIGILAR a PERSEGUIR.
        }
    }

    public override void Exit()
    {
        // Le resetear�amos la animaci�n de andar, o lo que sea...
        base.Exit();
    }
}