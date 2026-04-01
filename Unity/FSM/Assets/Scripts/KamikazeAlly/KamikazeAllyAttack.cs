using UnityEngine;
using UnityEngine.AI;

public class KamikazeAllyAttack : KamikazeAllyState
{
    public KamikazeAllyAttack(GameObject newAgentToSet, KamikazeAllyAI newAllyAI) : base(newAgentToSet, newAllyAI)
    {
        name = STATE.ATTACK; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entry();
        agent.GetComponent<Renderer>().material.color = Color.indianRed;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
        agent.GetComponent<KamikazeAllyAI>().Attack(nearestEnemy);
    }

    public override void Updating()
    {
        SearchForNearestEnemy();
        agent.transform.LookAt(player.transform.position);
        if (!IsAtAttackDistance())
        {
            nextState = new KamikazeAllyChase(agent, allyAI); // Si el NPC no puede atacar al jugador, lo ponemos a perseguir.
            actualFase = EVENT.EXIT; // Cambiamos de FASE ya que pasamos de ATACAR a PERSEGUIR.
        }
    }
}
