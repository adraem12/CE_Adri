using System.Collections;
using UnityEngine;

public class KamikazeAllyAI : MonoBehaviour
{
    KamikazeAllyState FSM;
    public float chaseDistance, attackDistance;

    void Start()
    {
        FSM = new KamikazeAllyPatrol(gameObject, this); // CREAMOS EL ESTADO INICIAL DEL NPC
    }

    void Update()
    {
        FSM = FSM.Process(); // Ejecutamos LA FSM
    }

    public void Attack(GameObject enemy)
    {
        DestroyImmediate(enemy);
        DestroyImmediate(gameObject);
    }
}
