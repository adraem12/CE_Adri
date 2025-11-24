/*
##################################

# Sánchez Sanz

# Adrià

# 20/11/2025

##################################
*/
using UnityEngine;

public class ConeScript : MonoBehaviour
{
    // Almacena los diferentes niveles de activación
    bool[] cheks = new bool[4] { false, false, false, false};

    void Update() // Ejercicio 7
    {
        if (GameManager.Instance.points.Count == 2 && !cheks[0]) // Solo se hace una vez
        {
            GetComponent<MeshRenderer>().material = GameManager.Instance.redMat;
            cheks[0] = true;
        }
        else if (GameManager.Instance.points.Count == 4 && !cheks[1])
        {
            transform.localScale *= 2;
            cheks[1] = true;
        }
        else if (GameManager.Instance.points.Count == 6 && !cheks[2])
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 8, ForceMode.Impulse);
            cheks[2] = true;
        }
        else if (GameManager.Instance.points.Count == 10 && !cheks[3])
        {
            cheks[3] = true;
            gameObject.SetActive(false);
        }
    }
}