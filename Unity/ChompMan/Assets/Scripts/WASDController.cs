using UnityEngine;

public class WASDController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float jumpSpeed;
    float vSpeed = 0;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        //Capturo el movimiento en los ejes
        float vMove = Input.GetAxis("Horizontal") * -1;
        float hMove = Input.GetAxis("Vertical");
        Vector3 angle = new(hMove, 0f, vMove);
        Vector3 movement = angle.normalized * speed;
        if (controller.isGrounded)
        {
            vSpeed = 0; // grounded character has vSpeed = 0...
            if (Input.GetKeyDown(KeyCode.Space)) // unless it jumps:
                vSpeed = jumpSpeed;
        }
        vSpeed -= 9.8f * Time.deltaTime;
        movement.y = vSpeed;
        //Genero el vector de movimiento
        //Muevo el jugador
        controller.Move(movement * Time.deltaTime);
        if (angle != null && angle != Vector3.zero)
        {
            transform.forward = angle * 1f;
            transform.rotation = Quaternion.LookRotation(angle);
        }
    }
}