using UnityEngine;

public class ActionPanelScript : MonoBehaviour
{
    public float time;
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Movement
        rectTransform.localPosition += Vector3.up * 0.1f;
        //Destruction
        time -= Time.deltaTime;
        if (time < 0)
            Destroy(gameObject);
    }
}