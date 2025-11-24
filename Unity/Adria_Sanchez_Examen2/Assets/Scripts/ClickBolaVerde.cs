/*
##################################

# Sánchez Sanz

# Adrià

# 20/11/2025

##################################
*/
using UnityEngine;

public class ClickBolaVerde : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.ClickBolaVerde();
    }
}