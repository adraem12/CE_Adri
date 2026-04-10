using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    NavMeshAgent agent;
    Controls controls;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controls = new();
        controls.Enable();
        controls.Player.Click.started += MovePlayer;
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(controls.Player.MousePosition.ReadValue<Vector2>()), out RaycastHit hit, 100))
            agent.destination = hit.point;
    }
}
