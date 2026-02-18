using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    CharacterController controller;
    Vector3 move = Vector3.zero;
    AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.controls.Player.Move.performed += ReadMoveInput;
        GameManager.instance.controls.Player.Move.canceled += ReadMoveInput;
    }

    void FixedUpdate()
    {
        move.Normalize();
        transform.Translate(speed * Time.deltaTime * move, Space.World); //Move player
        controller.Move(speed * Time.deltaTime * move);
        if (move != null && move != Vector3.zero) //Correct rotation
        {
            transform.forward = move * 1;
            transform.rotation = Quaternion.LookRotation(move);
        }
        if (controller.velocity.magnitude > 0.1f) //Control player sound
            audioSource.volume = 0.5f;
        else
            audioSource.volume = 0;
    }

    private void ReadMoveInput(InputAction.CallbackContext context)
    {
        move.x = context.ReadValue<Vector2>().x;
        move.z = context.ReadValue<Vector2>().y;
    }
}