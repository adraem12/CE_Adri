using System.Collections;
using UnityEngine;

public class EnemyAI: MonoBehaviour
{
    State FSM;

    void Start()
    {
        FSM = new Patrol(gameObject); // CREAMOS EL ESTADO INICIAL DEL NPC
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
            Debug.Log("attacando");
            yield return new WaitForSeconds(1);
        }
    }
}