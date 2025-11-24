/*
##################################

# Sánchez Sanz

# Adrià

# 20/11/2025

##################################
*/
using UnityEngine;

public class ClickBolaRoja : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.ClickBolaRoja();
    }
}