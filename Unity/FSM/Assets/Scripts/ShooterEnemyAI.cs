using System.Collections;
using UnityEngine;

public class ShooterEnemyAI: MonoBehaviour
{
    ShooterState FSM;
    public float chaseDistance, attackDistance, projectileForce, attackTimer;
    public GameObject projectilePrefab;

    void Start()
    {
        FSM = new ShooterPatrol(gameObject, this);
    }

    void Update()
    {
        FSM = FSM.Process();
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
            Rigidbody bullet = Instantiate(projectilePrefab, transform.position + transform.forward * 0.76f, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.AddForce((GameManager.instance.player.transform.position - transform.position).normalized * projectileForce, ForceMode.Impulse);
            yield return new WaitForSeconds(attackTimer);
        }
    }
}