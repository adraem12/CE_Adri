using UnityEngine;
using UnityEngine.InputSystem;

public class CrossScript : MonoBehaviour
{
    //Variables
    Transform cam;
    Vector3 moveVector;
    public float speed;
    public Vector2 maxMove;
    Vector2 localMaxMoveX;
    Vector2 localMaxMoveY;

    private void Awake()
    {
        cam = Camera.main.transform;
        // Define el máximo y el mínimo respecto a la posición inicial de la mirilla
        localMaxMoveX = new Vector2(transform.position.x - maxMove.x, maxMove.x + transform.position.x);
        localMaxMoveY = new Vector2(transform.position.y - maxMove.y, maxMove.y + transform.position.y);
    }

    void Start()
    {
        // Llama a la funcion cuando sucede
        GameManager.Instance.controls.Player.Move.performed += ReadMoveInput;
        GameManager.Instance.controls.Player.Move.canceled += ReadMoveInput;
    }
    void Update()
    {
        transform.LookAt(cam.position);
        Move();
    }

    void ReadMoveInput(InputAction.CallbackContext context) // Lee WASD
    {
        moveVector.x = context.ReadValue<Vector2>().x;
        moveVector.y = context.ReadValue<Vector2>().y;
        moveVector *= .01f * speed;
    }

    void Move() // Movimiento de la mirilla
    {
        Vector3 newPosition = transform.position + moveVector;
        transform.position = new Vector3(Mathf.Clamp(newPosition.x, localMaxMoveX.x, localMaxMoveX.y), Mathf.Clamp(newPosition.y, localMaxMoveY.x, localMaxMoveY.y), transform.position.z);
    }

    public Vector3 GetMaxMoveX()
    {
        return localMaxMoveX;
    }

    public Vector3 GetMaxMoveY()
    {
        return localMaxMoveY;
    }
}