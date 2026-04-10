using UnityEngine;
using UnityEngine.AI;

public class KamikazeAllyAI : MonoBehaviour
{
    KamikazeAllyState FSM;
    public float chaseDistance, attackDistance, explodeDistance;
    Animator animator;
    NavMeshAgent agent;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        FSM = new KamikazeAllyPatrol(gameObject, this); // CREAMOS EL ESTADO INICIAL DEL NPC
    }

    void Update()
    {
        FSM = FSM.Process(); // Ejecutamos LA FSM
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void Attack(GameObject enemy)
    {
        DestroyImmediate(enemy);
        DestroyImmediate(gameObject);
    }
}
