using UnityEngine;
using System.Text.RegularExpressions;

public class SquareScript : MonoBehaviour
{
    [HideInInspector] public int squareNum;

    private void Awake()
    {
        //Square info
        squareNum = int.Parse(Regex.Replace(gameObject.name, "[a-zA-Z]", ""));
    }
}