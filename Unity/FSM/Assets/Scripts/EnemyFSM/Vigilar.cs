using UnityEngine;

// Constructor para VIGILAR
public class Vigilar : State
{

    public Vigilar(GameObject newAgentToSet) : base(newAgentToSet)
    {
        Debug.Log("VIGILAR");
        name = STATE.VIGILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondriamos la animación de andar, calcular los puntos por los que patrulla, etc...

        base.Entry();
    }

    public override void Update()
    {
        // Le decimos que se vaya moviendo y patrullando...

        if (IsNearPlayer())
        {
            nextState = new Atacar(agent);
            actualFase = EVENT.SALIR; // Cambiamos de FASE ya que pasamos de VIGILAR a ATACAR.
        }
    }

    public override void Exit()
    {
        // Le resetear�amos la animaci�n de andar, o lo que sea...
        base.Exit();
    }

    // Comprueba si el enemigo está cerca
    public bool IsNearPlayer()
    {
        if (Vector3.Distance(agent.transform.position, GameManager.instance.player.transform.position) >= 10)
            return false;
        else
            return true;
    }
}