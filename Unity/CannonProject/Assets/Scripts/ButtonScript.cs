using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.Button1Down();
    }

    private void OnMouseUp()
    {
        GameManager.Instance.Button1Up();
    }
}