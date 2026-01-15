using UnityEngine;
using UnityEngine.InputSystem;

public class NavPlayer : MonoBehaviour
{
    public float speed;
    CharacterController controller ;
    Vector3 move = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        GameManager.instance.controls.Player.Move.performed += ReadMoveInput;
        GameManager.instance.controls.Player.Move.canceled += ReadMoveInput;
    }

    void FixedUpdate()
    {
        //Capturo el movimiento en los ejes
        move.Normalize();
        transform.Translate(speed * Time.deltaTime * move, Space.World);
        //Genero el vector de movimiento
        //Muevo el jugador
        controller.Move(speed * Time.deltaTime * move);
        if (move != null && move != Vector3.zero)
        {
            transform.forward = move * 1;
            transform.rotation = Quaternion.LookRotation(move);
        }
    }

    private void ReadMoveInput(InputAction.CallbackContext context)
    {
        move.x = context.ReadValue<Vector2>().x;
        move.z = context.ReadValue<Vector2>().y;
    }
}