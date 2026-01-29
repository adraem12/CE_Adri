using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI fingerText;
    public TextMeshProUGUI actionText;
    public ARRaycastManager raycastManager;

    void Update()
    {
        int screenFingers = Input.touchCount;
        fingerText.text = screenFingers.ToString();
        if (screenFingers > 0)
        {
            Touch firstFinger = Input.GetTouch(0);
            actionText.text = firstFinger.phase.ToString();
        }
    }

    public void InstancePrefab()
    {
        List<ARRaycastHit> raycastHits = new();
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), raycastHits, TrackableType.Planes))
        {
            Instantiate(raycastManager.raycastPrefab, raycastHits[0].pose.position, Quaternion.identity);
        }
    }
}