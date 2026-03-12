using System.Collections;
using UnityEngine;

public class ShooterEnemyAI: MonoBehaviour
{
    ShooterState FSM;
    public float chaseDistance, attackDistance, projectileForce;
    public GameObject projectilePrefab;
    float attackTimer;

    void Start()
    {
        FSM = new ShooterPatrol(gameObject, this); // CREAMOS EL ESTADO INICIAL DEL NPC
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
            Rigidbody bullet = Instantiate(projectilePrefab, transform.position + transform.forward * 0.75f, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.AddForce((GameManager.instance.player.transform.position - transform.position).normalized * projectileForce, ForceMode.Impulse);
            yield return new WaitForSeconds(1.5f);
        }
    }
}