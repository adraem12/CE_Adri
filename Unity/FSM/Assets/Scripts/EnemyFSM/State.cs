using UnityEngine;

public class State
{
    protected GameObject agent;
    protected GameObject player;
    public float attackDistance, chaseDistance;

    // 'ESTADOS' que tiene el NPC
    public enum STATE { PATROL, ATTACK, CHASE };

    // 'EVENTOS' - En que parte nos encontramos del estado
    public enum EVENT { ENTER, UPDATE, EXIT };
    public STATE name; // Para guardar el nombre del estado
    protected EVENT actualFase; // Para guardar la fase en la que nos encontramos
    protected State nextState; // El estado que se EJECUTARÁ A CONTINUACIÓN del estado actual

    // Constructor
    public State(GameObject agentToSet) 
    { 
        agent = agentToSet; 
        player = GameManager.instance.player;
        attackDistance = 1.5f;
        chaseDistance = 10;
    }

    // Las fases de cada estado
    public virtual void Entry() { actualFase = EVENT.UPDATE; } // La primera fase que se ejecuta cuando cambiamos de estado. El siguiente estado debería ser "actualizar".
    public virtual void Updating() { actualFase = EVENT.UPDATE; } // Una vez estas en ACTUALIZAR, te quedas en ACTUALIZAR hasta que quieras cambiar de estado.
    public virtual void Exit() { actualFase = EVENT.EXIT; } // La fase de SALIR es la última antes de cambiar de ESTADO, aquí deberiamos limpiar lo que haga falta.

    // Este es la función a la que llamaremos para que el NPC inicie la máquina de estados. Vincula los EVENTOS con las funciones que ejecuta cada uno
    public State Process()
    {
        if (actualFase == EVENT.ENTER) 
            Entry();
        if (actualFase == EVENT.UPDATE) 
            Updating();
        if (actualFase == EVENT.EXIT)
        {
            Exit();
            return nextState; // IMPORTANTE: Aquí hacemos el cambio de estado.
        }
        return this; // Si no salimos por el return de arriba, seguimos en el mismo estado.
    }

    // Comprueba si el enemigo está cerca
    protected bool IsAtAttackDistance()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) >= attackDistance)
            return false;
        else
            return true;
    }

    // Comprueba si el enemigo está visible
    protected bool IsAtChaseDistance()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) >= chaseDistance)
            return false;
        else
            return true;
    }
}