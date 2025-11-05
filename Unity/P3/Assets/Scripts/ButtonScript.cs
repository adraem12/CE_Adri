using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (name.Contains("1"))
            GameManager.Instance.Button1();
        else if (name.Contains("2"))
            GameManager.Instance.Button2();
        else if (name.Contains("3"))
            GameManager.Instance.Button3();
    }
}