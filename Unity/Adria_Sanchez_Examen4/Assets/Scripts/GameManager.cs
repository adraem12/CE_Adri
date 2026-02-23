using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/********************************************
 *          MOTORES DE VIDEOJUEGOS          *
 *                                          *
 *                23/02/26                  *
 *                                          *
 *    Nombre: Adrià Sánchez Sanz            *
 *                                          *
 ********************************************/

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int[] squareState;
    public GameObject[] squareObjects;
    public Transform[] squarePositions;
    int emptySquares;
    [HideInInspector] public bool gameEnd;
    [HideInInspector] public int turn;
    [HideInInspector] public int round;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI emptySquareText;
    public GameObject endPanel;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //  NO TOCAR - INICIALIZAR LOS VECTORES
        squareState = new int[36];
        squareObjects = new GameObject[36];
        squarePositions = new Transform[36];
        emptySquares = 36;
        for (int i = 0; i < 36; i++)
        {
            squareState[i] = 0; // CASILLA VACÍA
            squareObjects[i] = GameObject.Find("casilla" + i);
            squarePositions[i] = squareObjects[i].transform;
        }
        //FIN NO TOCAR 
        turn = 1;
        round = 1;
        SetText(string.Empty, "Round " + round, Color.white);
        StartCoroutine(TurnSystem());
    }

    IEnumerator TurnSystem() 
    {
        // PUNTO 1 – INICIO
        for (int i = 1; i <= 4; i++)
        {
            int newSquare = Random.Range(0, squareObjects.Length);
            while (squareState[newSquare] != 0)
                newSquare = Random.Range(0, squareObjects.Length);
            yield return new WaitForSeconds(1f);
            if (i % 2 == 0)
            {
                squareObjects[newSquare].GetComponent<MeshRenderer>().material.color = Color.green;
                squareState[newSquare] = 2;
                emptySquares--;
            }
            else
            {
                squareObjects[newSquare].GetComponent<MeshRenderer>().material.color = Color.red;
                squareState[newSquare] = 1;
                emptySquares--;
            }
            SetText(string.Empty, string.Empty, Color.white);
        }
        while (!gameEnd)
        {
            //Tirada jugador
            SetText("Player turn", string.Empty, Color.red);
            yield return new WaitUntil(() => turn == 2);
            emptySquares--;
            if (gameEnd)
            {
                SetText("You win!", string.Empty, Color.red);
                break;
            }
            // PUNTO 4 - TIRADA DE LA IA
            SetText("AI turn", string.Empty, Color.green);
            for (int i = 1; i <= 2; i++)
            {
                int newSquare = Random.Range(0, squareObjects.Length);
                while (squareState[newSquare] != 0)
                    newSquare = Random.Range(0, squareObjects.Length);
                yield return new WaitForSeconds(1f);
                squareObjects[newSquare].GetComponent<MeshRenderer>().material.color = Color.green;
                squareState[newSquare] = 2;
                emptySquares--;
                SetText(string.Empty, string.Empty, Color.green);
                if (ComprobarGanador(newSquare))
                {
                    gameEnd = true;
                    break;
                }
            }
            if (gameEnd)
                SetText("You lose :(", string.Empty, Color.green);
            else
                turn = 1;
            yield return new WaitForSeconds(1f);
            round++;
            SetText(string.Empty, "Round " + round, Color.white);
        }
        endPanel.SetActive(true);
        turn = 0;
    }

    void SetText(string newTurnText, string newRoundText, Color color)
    {
        if (newTurnText != string.Empty)
            turnText.text = newTurnText;
        if (color != Color.white)
            turnText.color = color;
        if (newRoundText != string.Empty)
            roundText.text = newRoundText;
        emptySquareText.text = "Empty squares: " + emptySquares;
    }

    public void ReplayButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    static int[,] ConvertirMatriz(int[] arr, int m, int n)
    {
        // INTENTO 1 - FAIL NO EFICIENTE
        /*int[,] matrizTablero = new int[6, 6];

        int fila = 0, columna = 0;

        for (int i = 0; i < 36; i++)
        {
            matrizTablero[fila, columna] = vectorCasillas[i];

            if (i == 5 || i == 11 || i == 17 || i == 23 || i == 29)
            {
                columna++;
                fila = 0;
            }
        }
        */

        // INTENTO 2 - WIN GG
        int[,] matrizTablero = new int[m, n];
        for (int i = 0; i < arr.Length; i++)
        {
            matrizTablero[i % m, i / m] = arr[i];
        }
        return matrizTablero;
    }

    static void MostrarMatriz(int[,] matrix)
    {
        string tmp = "";

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {

                if (matrix[i, j] == 0)
                    tmp += "-";
                else if (matrix[i, j] == 1)
                    tmp += "X";
                else if (matrix[i, j] == 2)
                    tmp += "O";
                // tmp += matrizTablero[i, j] + " ";
            }
            Debug.Log("COLUMNA " + i + " - " + tmp);
            tmp = "";
        }
    }

    public bool ComprobarGanador(int casilla)
    {
        // TIRADA ACTUAL
        int x = casilla % 6;
        int y = casilla / 6;

        // OPCIï¿½N A CHECKEAR
        int quien = squareState[casilla];

        // A CONVERTIR LA MATRIZ
        int[,] matrizTablero = ConvertirMatriz(squareState, 6, 6);

        //check columna
        int seguidasCol = 0;
        for (int i = 0; i < 6; i++)
        {
            if (matrizTablero[x, i] == quien)
                seguidasCol++;
            else
                seguidasCol = 0;

            if (seguidasCol == 4)
            {
                ResaltarColumna(x);
                return true; // Ganamos 
            }
        }

        //check fila
        int seguidasFil = 0;
        for (int i = 0; i < 6; i++)
        {
            if (matrizTablero[i, y] == quien) seguidasFil++;
            else seguidasFil = 0;
            if (seguidasFil == 4)
            {
                ResaltarFila(y);
                return true; // Ganamos
            }
        }

        //check diagonal 1
        int seguidasDi1 = 0;
        int xd1 = x, yd1 = y;
        while (xd1 > 0 && yd1 > 0) // Nos movemos al inicio de la diagonal
        {
            xd1--;
            yd1--;
        }
        int resaltarx1 = xd1;
        int resaltary1 = yd1;
        int cont1 = 0;
        bool final1 = false;
        while (cont1 < 6 && !final1) // Y contamos ...
        {
            if (matrizTablero[xd1, yd1] == quien)
                seguidasDi1++;
            else
                seguidasDi1 = 0;
            xd1++;
            yd1++;

            if (seguidasDi1 == 4)
            {
                ResaltarDiagonal1(resaltarx1, resaltary1);
                return true; // Ganamos
            }

            if (xd1 > 5 || yd1 > 5)
                final1 = true; // Fin de la diagonal -> salir del bucle while
            cont1++;
        }

        //check diagonal 2
        int seguidasDi2 = 0;
        int xd2 = x, yd2 = y;
        while (xd2 > 0 && yd2 < 5) // Nos movemos al inicio de la diagonal
        {
            xd2--;
            yd2++;
        }
        int resaltarx2 = xd2;
        int resaltary2 = yd2;
        int cont2 = 0;
        bool final2 = false;
        while (cont2 < 6 && !final2) // Y contamos ...
        {
            if (matrizTablero[xd2, yd2] == quien)
                seguidasDi2++;
            else
                seguidasDi2 = 0;
            xd2++;
            yd2--;

            if (seguidasDi2 == 4)
            {
                ResaltarDiagonal2(resaltarx2, resaltary2);
                return true; // Ganamos
            }
            if (xd2 > 5 || yd2 < 0)
                final2 = true; // Fin de la diagonal -> salir del bucle while
            cont2++;
        }

        return false; // No hemos ganado
    }

    private void ResaltarDiagonal1(int resaltarX, int resaltarY)
    {
        while (resaltarX < 6 && resaltarY < 6)
        {
            squareObjects[resaltarY * 6 + resaltarX].GetComponent<Renderer>().material.color = Color.blue;
            resaltarX++;
            resaltarY++;
        }
    }

    private void ResaltarDiagonal2(int resaltarX, int resaltarY)
    {
        while (resaltarX < 6 && resaltarY >= 0)
        {
            squareObjects[resaltarY * 6 + resaltarX].GetComponent<Renderer>().material.color = Color.blue;
            resaltarX++;
            resaltarY--;
        }
    }

    private void ResaltarColumna(int x)
    {
        for (int ii = 0; ii < 6; ii++)
        {
            squareObjects[ii * 6 + x].GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void ResaltarFila(int y)
    {
        for (int ii = 0; ii < 6; ii++)
        {
            squareObjects[y * 6 + ii].GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}