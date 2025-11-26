using UnityEngine;

public class DestroyByTimeScript : MonoBehaviour
{
    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(gameObject);
    }
}