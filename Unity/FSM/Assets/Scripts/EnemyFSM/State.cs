using UnityEngine;

public class State
{
    protected GameObject agent;

    // 'ESTADOS' que tiene el NPC
    public enum STATE { VIGILAR, ATACAR };

    // 'EVENTOS' - En que parte nos encontramos del estado
    public enum EVENT { ENTRAR, ACTUALIZAR, SALIR };
    public STATE name; // Para guardar el nombre del estado
    protected EVENT actualFase; // Para guardar la fase en la que nos encontramos
    protected State nextState; // El estado que se EJECUTARÁ A CONTINUACIÓN del estado actual

    // Constructor
    public State(GameObject agentToSet) { agent = agentToSet; }

    // Las fases de cada estado
    public virtual void Entry() { actualFase = EVENT.ACTUALIZAR; } // La primera fase que se ejecuta cuando cambiamos de estado. El siguiente estado debería ser "actualizar".
    public virtual void Update() { actualFase = EVENT.ACTUALIZAR; } // Una vez estas en ACTUALIZAR, te quedas en ACTUALIZAR hasta que quieras cambiar de estado.
    public virtual void Exit() { actualFase = EVENT.SALIR; } // La fase de SALIR es la última antes de cambiar de ESTADO, aquí deberiamos limpiar lo que haga falta.

    // Este es la función a la que llamaremos para que el NPC inicie la máquina de estados. Vincula los EVENTOS con las funciones que ejecuta cada uno
    public State Process()
    {
        if (actualFase == EVENT.ENTRAR) 
            Entry();
        if (actualFase == EVENT.ACTUALIZAR) 
            Update();
        if (actualFase == EVENT.SALIR)
        {
            Exit();
            return nextState; // IMPORTANTE: Aquí hacemos el cambio de estado.
        }
        return this; // Si no salimos por el return de arriba, seguimos en el mismo estado.
    }
}