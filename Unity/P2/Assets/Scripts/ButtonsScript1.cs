using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript1 : MonoBehaviour
{
    //Declaración de variables
    Material material;
    Vector3 direction;
    readonly List<MovableScript> spheres = new();

    private void Awake()
    {
        //Asignación de variables antes de que se renderice el primer frame
        GameObject[] sphereObjects = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject obj in sphereObjects)
            spheres.Add(obj.GetComponent<MovableScript>());
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
            if (material.color == Color.red) //Mueve el objeto a su posición inicial
                foreach (MovableScript obj in spheres)
                {
                    obj.speed = 30;
                    obj.newPosition = obj.startPosition;
                }
            else if (material.color == Color.black) //Activa y desactiva el objeto
            {
                foreach (MovableScript obj in spheres)
                    if (obj.gameObject.activeSelf)
                        obj.gameObject.SetActive(false);
                    else
                        obj.gameObject.SetActive(true);
            }
        }
        else
        {
            if (material.color == Color.white) //Movimiento x1
                foreach (MovableScript obj in spheres)
                    obj.newPosition += direction;
            else if (material.color == Color.green) //Movimiento x3
                foreach (MovableScript obj in spheres)
                    obj.newPosition += direction * 3;
        }
    }
}