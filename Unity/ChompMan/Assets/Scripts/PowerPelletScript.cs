using UnityEngine;
using UnityEngine.AI;

public class PowerPelletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            var agents = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);
            foreach (var agent in agents)
                Destroy(agent.gameObject);
            Destroy(gameObject);
        }
    }
}