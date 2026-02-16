using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float time;
    private void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
            Destroy(gameObject);
    }
}