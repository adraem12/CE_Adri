using System.Collections;
using UnityEngine;

public class BullseyeScript : MonoBehaviour
{
    int collisionCount = 0;
    public float rotationSpeed;

    private void Update()
    {
        if (collisionCount == 1)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collisionCount)
        {
            case 0:
                GetComponent<MeshRenderer>().material.color = Color.red;
                collisionCount++;
                break;
            case 1:
                collisionCount++;
                break;
            case 2:
                transform.rotation = Quaternion.Euler(90, 0, 0);
                Destroy(gameObject);
                collisionCount++;
                break;
        }
    }
}