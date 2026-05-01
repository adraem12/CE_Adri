using UnityEngine;

public class BinScript : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;

    public float moveSpeed;
    Vector3 current;
    Vector3 target;
    float sinTime;
    
    void Start()
    {
        current = pointA;
        target = pointB;
        transform.position = current;
    }

    void Update()
    {
        if (transform.position != target)
        {
            sinTime += Time.deltaTime * moveSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
            transform.position = Vector3.Lerp(current, target, t);
        }
        Swap();
    }

    void Swap()
    {
        if (transform.position != target)
            return;
        (target, current) = (current, target);
        sinTime = 0;
    }
}