using UnityEngine;

public class MovableScript : MonoBehaviour
{
    //Declaración de variables
    [HideInInspector] public Vector3 newPosition;
    [HideInInspector] public Vector3 startPosition;
    public int speed = 3;

    void Awake()
    {
        //Asignación de variables antes de que se renderice el primer frame
        startPosition = transform.position;
        newPosition = startPosition;
    }

    void Update()
    {
        if (transform.position != newPosition) //Mueve el objeto a la posición deseada
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
        else speed = 3; //Resetea la velocidad
    }
}