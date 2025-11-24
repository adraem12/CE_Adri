/*
##################################

# Sánchez Sanz

# Adrià

# 20/11/2025

##################################
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager Instance { get; private set; }
    public GameObject bolaRoja;
    public GameObject bolaVerde;
    public GameObject victoryPoint;
    public GameObject alarmsParent;
    MeshRenderer[] alarms;
    public Material redMat;
    public Material greenMat;
    [HideInInspector] public List<GameObject> points = new();

    private void Awake()
    {
        Instance = this; // Crea la instancia del GameManager
        alarms = alarmsParent.GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {   
        if (Vector3.Distance(bolaRoja.transform.position, bolaVerde.transform.position) < 5) // Ejercicio 5
        {
            foreach (var alarm in alarms)
                if (alarm.material != redMat) alarm.material = redMat; // Checkea si ya ha cambiado el material
        }
        else
        {
            foreach (var alarm in alarms)
                if (alarm.material != greenMat) alarm.material = greenMat;
        }
    }

    public void ClickBolaRoja()
    {
        bolaVerde.transform.localScale /= 2; // Ejercicio 3
        if (points.Count > 0) // Ejercicio 9
        {
            GameObject pointToDestroy = points.Last(); // Se elimina de la lista antes de destruirlo
            points.Remove(pointToDestroy);
            Destroy(pointToDestroy);
        }
    }

    public void ClickBolaVerde()
    {
        /*Instantiate(victoryPoint, new Vector3(-20.5f, 0, 0), Quaternion.identity);*/ // Ejercicio 4
        GameObject newPoint = Instantiate(victoryPoint, new Vector3(16.5f - (2 * points.Count), -3, -17), Quaternion.identity); // Ejercicio 8
        newPoint.GetComponentInChildren<Rigidbody>().isKinematic = true;
        points.Add(newPoint);
    }
}