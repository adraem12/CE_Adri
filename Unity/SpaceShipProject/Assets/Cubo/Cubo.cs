using UnityEngine;
using UnityEngine.InputSystem;

public class Cubo : MonoBehaviour
{
    public float speed = 15f;
    public float jumpForce = 15f;
    Vector3 moveVector = Vector3.zero;
    Rigidbody rb;
    public bool grounded = true;

    void Start()
    {
        GameManager.Instance.controls.Player.Move.performed += ReadMoveInput;
        GameManager.Instance.controls.Player.Move.canceled += ReadMoveInput;
        GameManager.Instance.controls.Player.Jump.performed += ReadJumpInput;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveVector.x, 0, moveVector.y) * speed);
        //transform.Translate(new Vector3(moveVector.x, 0, moveVector.y) * speed);
    }

    void ReadMoveInput(InputAction.CallbackContext context) // Lee WASD
    {
        moveVector.x = context.ReadValue<Vector2>().x;
        moveVector.y = context.ReadValue<Vector2>().y;
        moveVector *= 10 * speed;
    }

    void ReadJumpInput(InputAction.CallbackContext context) // Lee WASD
    {
        if (grounded)
            rb.AddForce(jumpForce * 10 * Vector3.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}