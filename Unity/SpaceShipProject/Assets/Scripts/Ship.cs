using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public float speed = 7f;
    public float rotationforce = 1.5f;
    public float dashMultiplier = 3f;
    public float dashDuration = 2;
    public float dashCooldownTimer = 4;
    float currentDashDuration;
    float currentDashCooldownTimer;
    float currentSpeed;
    public Renderer rightEngine;
    public Renderer leftEngine;
    private Color originalColor;
    private bool dash = false;
    Vector3 moveVector = Vector3.zero;
    Vector3 rotateVector = Vector3.zero;

    private void Awake()
    {
        originalColor = rightEngine.material.color;
        currentSpeed = speed;
    }

    void Start()
    {
        // Llama a la funcion cuando sucede
        GameManager.Instance.controls.Player.Move.performed += ReadMoveInput;
        GameManager.Instance.controls.Player.Move.canceled += ReadMoveInput;
        GameManager.Instance.controls.Player.Rotate.performed += ReadRotateInput;
        GameManager.Instance.controls.Player.Rotate.canceled += ReadRotateInput;
        GameManager.Instance.controls.Player.Sprint.started += ReadDashInput;
    }

    void FixedUpdate()
    {
         ApplyRotation();
         ApplyMovement();
    }

    private void ApplyMovement()
    {
        transform.Translate(new Vector3(moveVector.x, 0, moveVector.y) * currentSpeed);
    }

    private void ApplyRotation()
    {
        float xRotate = 0;
        float yRotate = 0;

        if (rotateVector.x > 0)
            xRotate = rotationforce;
        else if (rotateVector.x < 0)
            xRotate = -rotationforce;
        if (rotateVector.y > 0)
            yRotate = rotationforce;
        else if (rotateVector.y < 0)
            yRotate = -rotationforce;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + yRotate, transform.eulerAngles.y + xRotate, transform.eulerAngles.z);
    }

    void ReadMoveInput(InputAction.CallbackContext context) // Lee WASD
    {
        moveVector.x = context.ReadValue<Vector2>().x;
        moveVector.y = context.ReadValue<Vector2>().y;
        moveVector *= .01f * speed;
    }

    void ReadRotateInput(InputAction.CallbackContext context) // Lee WASD
    {
        rotateVector.x = context.ReadValue<Vector2>().x;
        rotateVector.y = context.ReadValue<Vector2>().y;
        rotateVector *= .01f * speed;
    }

    void ReadDashInput(InputAction.CallbackContext context) // Lee WASD
    {
        if (!dash)
        {
            dash = true;
            currentSpeed = currentSpeed * dashMultiplier;
            currentDashDuration = dashDuration;
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        while (currentDashDuration > 0)
        {
            currentDashDuration -= Time.deltaTime;
            yield return null;
        }
        currentSpeed = speed;
        currentDashCooldownTimer = dashCooldownTimer;
        StartCoroutine(DashCooldownCoroutine());
    }

    IEnumerator DashCooldownCoroutine()
    {
        while (currentDashCooldownTimer > 0)
        {
            currentDashCooldownTimer -= Time.deltaTime;
            yield return null;
        }
        dash = false;
        Debug.Log("Dash cooled down");
    }
}