using UnityEngine;

public class BolaVerde : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 1f, 0));
    }
}