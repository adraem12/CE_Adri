using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    //Variables
    public float speed = 7f;
    public float rotationforce = 1.75f;
    public float dashMultiplier = 3f;
    public float dashDuration = 2;
    public float dashCooldownTimer = 4;
    public CinemachineCamera cam;
    float currentDashDuration;
    float currentDashCooldownTimer;
    float currentSpeed;
    float currentRotationForce;
    Rigidbody rb;
    bool dash = false;
    float moveForce = 0;
    Vector3 rotateVector = Vector3.zero;

    private void Awake()
    {
        currentSpeed = speed;
        currentRotationForce = rotationforce;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //Llama a la función cuando sucede
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

    void ApplyMovement()
    {
        rb.AddRelativeForce(new Vector3(0, 0, moveForce) * currentSpeed);
    }

    void ApplyRotation()
    {
        //Limita y calcula las rotaciones y los balanceos
        float xRotate = 0;
        float yRotate = 0;
        float tilt;
        Vector3 newRot = transform.eulerAngles;
        newRot.x = Mathf.Clamp((newRot.x > 180f) ? newRot.x - 360f : newRot.x, -45f, 45f);
        newRot.z = Mathf.Clamp((newRot.z > 180f) ? newRot.z - 360f : newRot.z, -55f, 55f);
        if (rotateVector.x > 0)
            xRotate = currentRotationForce;
        else if (rotateVector.x < 0)
            xRotate = -currentRotationForce;
        if (rotateVector.y > 0)
            yRotate = currentRotationForce;
        else if (rotateVector.y < 0)
            yRotate = -currentRotationForce;
        if (rotateVector.x != 0)
        {
            if (Mathf.Sign(rotateVector.x) == Mathf.Sign(newRot.z))
                tilt = currentRotationForce * Time.deltaTime * -rotateVector.x * 1000f;
            else
                tilt = currentRotationForce * Time.deltaTime * -rotateVector.x * 500f;
        }
        else
            tilt = -newRot.z * currentRotationForce * Time.deltaTime;
        newRot += new Vector3(yRotate, xRotate, tilt);
        transform.rotation = Quaternion.Euler(newRot);
    }

    void ReadMoveInput(InputAction.CallbackContext context) //Lee WS
    {
        moveForce = context.ReadValue<Vector2>().y;
        moveForce *= 100 * speed;
    }

    void ReadRotateInput(InputAction.CallbackContext context) //Lee las flechas de dirección
    {
        rotateVector.x = context.ReadValue<Vector2>().x;
        rotateVector.y = context.ReadValue<Vector2>().y;
        rotateVector *= .01f * speed;
    }

    void ReadDashInput(InputAction.CallbackContext context) //Lógica del impulso
    {
        if (!dash)
        {
            dash = true;
            rb.AddExplosionForce(100, transform.position - (transform.forward * 2f), 6f);
            currentSpeed *= dashMultiplier;
            currentRotationForce *= dashMultiplier * 0.5f;
            currentDashDuration = dashDuration;
            StartCoroutine(DashCoroutine());
            StartCoroutine(DashCamFOV(80));
        }
    }

    IEnumerator DashCoroutine()
    {
        while (currentDashDuration > 0)
        {
            currentDashDuration -= Time.deltaTime;
            UIManager.Instance.DashSliderUpdate(dashDuration, currentDashDuration, true);
            yield return null;
        }
        StartCoroutine(DashCamFOV(60));
        currentSpeed = speed;
        currentRotationForce = rotationforce;
        currentDashCooldownTimer = 0;
        StartCoroutine(DashCooldownCoroutine());
    }

    IEnumerator DashCooldownCoroutine()
    {
        while (currentDashCooldownTimer < dashCooldownTimer)
        {
            currentDashCooldownTimer += Time.deltaTime;
            UIManager.Instance.DashSliderUpdate(dashCooldownTimer, currentDashCooldownTimer, false);
            yield return null;
        }
        dash = false;
    }

    IEnumerator DashCamFOV(float endFov)
    {
        while (cam.Lens.FieldOfView != endFov)
        {
            cam.Lens.FieldOfView = Mathf.MoveTowards(cam.Lens.FieldOfView, endFov, 50f * Time.deltaTime);
            yield return null;
        }
    }
}