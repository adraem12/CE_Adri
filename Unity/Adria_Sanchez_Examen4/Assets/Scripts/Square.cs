using UnityEngine;

/********************************************
 *          MOTORES DE VIDEOJUEGOS          *
 *                                          *
 *                23/02/26                  *
 *                                          *
 *    Nombre: Adrià Sánchez Sanz            *
 *                                          *
 ********************************************/

public class Square : MonoBehaviour
{
    // VARIABLE ENTERA QUE GUARDA EL NUMERO DE CASILLA
    public int squareNumber;

    void Start()
    {
        // NO TOCAR -- RELLENAMOS EL NUMERO CASILLA CON SU Nº CORRESPONDIENTE
        string squareName = gameObject.name.Substring(7);
        squareNumber = int.Parse(squareName);
    }

    // EJERCICIO 2 - TIRADA HUMANO
    private void OnMouseDown()
    {
        if (GameManager.instance.squareState[squareNumber] == 0 && GameManager.instance.turn == 1)
        {
            GameManager.instance.squareState[squareNumber] = 1;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            if (GameManager.instance.ComprobarGanador(squareNumber))
                GameManager.instance.gameEnd = true;
            GameManager.instance.turn = 2;
        }
    }
}