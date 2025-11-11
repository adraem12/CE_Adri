using UnityEngine;

public class CrossScript : MonoBehaviour
{
    Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(cam.position);
    }
}