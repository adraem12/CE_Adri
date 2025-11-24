/*
##################################

# Sánchez Sanz

# Adrià

# 20/11/2025

##################################
*/
using UnityEngine;

public class CountButtonScript : MonoBehaviour
{
    private void OnMouseDown() // Ejercicio 6
    {
        int points = GameObject.FindGameObjectsWithTag("VictoryPoint").Length;
        Debug.Log("Tienes " + points + " puntos");
    }
}