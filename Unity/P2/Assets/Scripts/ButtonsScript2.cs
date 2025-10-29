using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript2 : MonoBehaviour
{
    //Declaración de variables
    Material material;
    Vector3 direction = new(0, 0, 0);
    readonly List<MovableScript> sphereObj = new();
    readonly List<MovableScript> cubeObj = new();

    private void Awake()
    {
        //Asignación de variables antes de que se renderice el primer frame
        GameObject[] sphereObjects = GameObject.FindGameObjectsWithTag("Sphere");
        GameObject[] cubeObjects = GameObject.FindGameObjectsWithTag("Cube");
        foreach (GameObject obj in cubeObjects)
            cubeObj.Add(obj.GetComponent<MovableScript>());
        foreach (GameObject obj in sphereObjects)
            sphereObj.Add(obj.GetComponent<MovableScript>());
        material = GetComponent<MeshRenderer>().material;
        if (name.Contains("Down"))
            direction = new Vector3(0, -1);
        else if (name.Contains("Middle"))
            direction = new Vector3(0, 0);
        else if (name.Contains("Up"))
            direction = new Vector3(0, 1);
        else if (name.Contains("Left"))
            direction = new Vector3(-1, 0);
        else if (name.Contains("Right"))
            direction = new Vector3(1, 0);
    }

    private void OnMouseDown()
    {
        //Diferenciación entre botón de movimiento y el resto
        if (direction == new Vector3(0, 0))
        {
            if (material.color == Color.red) //Mueve los cubos a sus respectivas posiciones iniciales
                foreach (MovableScript obj in cubeObj)
                {
                    obj.speed = 30;
                    obj.newPosition = obj.startPosition;
                }
            else if (material.color == Color.blue) //Desactiva los objetos
            {
                foreach (MovableScript obj in sphereObj)
                    obj.gameObject.SetActive(false);
                foreach (MovableScript obj in cubeObj)
                    obj.gameObject.SetActive(false);
            }
            else if (material.color == Color.green) //Activa los objetos
            {
                foreach (MovableScript obj in sphereObj)
                    obj.gameObject.SetActive(true);
                foreach (MovableScript obj in cubeObj)
                    obj.gameObject.SetActive(true);
            }
            if (material.color == Color.white) //Randomiza el color de las esferas
                foreach (MovableScript obj in sphereObj)
                    obj.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1f, 0, 1f, 0, 1f, 1, 1);
        }
        else
        {
            if (material.color == Color.black) //Movimiento de las esferas
                foreach (MovableScript obj in sphereObj)
                    obj.newPosition += direction;
            else if (material.color == Color.gray) //Movimiento de los cubos
                foreach (MovableScript obj in cubeObj)
                    obj.newPosition += direction;
        }
    }
}