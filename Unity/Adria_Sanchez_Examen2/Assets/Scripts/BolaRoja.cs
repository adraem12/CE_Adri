using UnityEngine;

public class BolaRoja : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-120, 120), 0, Random.Range(-120, 120)));
    }
}