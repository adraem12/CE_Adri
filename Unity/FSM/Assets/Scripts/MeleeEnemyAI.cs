using System.Collections;
using UnityEngine;

public class MeleeEnemyAI: MonoBehaviour
{
    MeleeState FSM;
    public float chaseDistance, attackDistance;

    void Start()
    {
        FSM = new MeleePatrol(gameObject, this); // CREAMOS EL ESTADO INICIAL DEL NPC
    }

    void Update()
    {
        FSM = FSM.Process(); // Ejecutamos LA FSM
    }

    public void StartAttacking()
    {
        StartCoroutine(AttackRoutine());
    }

    public void StopAttacking()
    {
        StopAllCoroutines();
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            Debug.Log("atacando");
            yield return new WaitForSeconds(1);
        }
    }
}