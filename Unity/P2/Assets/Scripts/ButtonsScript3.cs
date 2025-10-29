using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript3 : MonoBehaviour
{
    //Declaración de variables
    Material material;
    readonly List<MovableScript> movObjects = new();

    private void Awake()
    {
        //Asignación de variables antes de que se renderice el primer frame
        MovableScript[] movableObjects = GameObject.Find("MovObjects").GetComponentsInChildren<MovableScript>();
        foreach (var movableObject in movableObjects)
            movObjects.Add(movableObject);
        material = GetComponent<MeshRenderer>().material;
    }

    private void OnMouseDown()
    {
        //Asignación de función de los botones
        if (material.color == Color.blue) //Movimiento aleatorio asegurado
            foreach (var movableObject in movObjects)
            {
                int rndm = Random.Range(1, 4);
                Vector3 direction = Vector3.zero;
                switch (rndm) //Elige una dirección
                {
                    case 1:
                        direction = Vector3.right;
                        break;
                    case 2:
                        direction = Vector3.up;
                        break;
                    case 3:
                        direction = Vector3.forward;
                        break;
                }
                movableObject.newPosition += direction * (Random.Range(0, 2) * 2 - 1); //Elige si el movimiento es positivo o negativo
            }
        else if (material.color == Color.green)
        {
            foreach (var movableObject in movObjects)
                if (Random.Range(0, 2) == 0) //Elige si el objeto se mueve
                {
                    int rndm = Random.Range(1, 4);
                    Vector3 direction = Vector3.zero;
                    switch (rndm) //Elige una dirección
                    {
                        case 1:
                            direction = Vector3.right;
                            break;
                        case 2:
                            direction = Vector3.up;
                            break;
                        case 3:
                            direction = Vector3.forward;
                            break;
                    }
                    movableObject.newPosition += direction * (Random.Range(0, 2) * 2 - 1); //Elige si el movimiento es positivo o negativo
                }
        }
        else if (material.color == Color.red) //Mueve los objetos a sus respectivas posiciones iniciales
            foreach (var movableObject in movObjects)
            {
                movableObject.speed = 30;
                movableObject.newPosition = movableObject.startPosition;
            }
    }
}