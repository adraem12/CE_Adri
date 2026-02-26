using UnityEngine;

public class Atacar : State
{
    public Atacar(GameObject newAgentToSet) : base(newAgentToSet)
    {
        Debug.Log("ATACAR");
        name = STATE.ATACAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entry()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        base.Entry();
    }

    public override void Update()
    {
        if (!PuedeAtacar())
        {
            nextState = new Vigilar(agent); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
            actualFase = EVENT.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.
        }
    }

    public override void Exit()
    {
        // Le resetearíamos la animación de disparar, o lo que sea...
        base.Exit();
    }

    public bool PuedeAtacar()
    {
        // ...
        return true; // El NPC NO ESTÁ lo suficientemente cerca para atacar al jugador.
    }
}