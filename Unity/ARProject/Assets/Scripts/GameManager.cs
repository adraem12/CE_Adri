using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI planeCountText;
    public TextMeshProUGUI prefabCountText;
    public TextMeshProUGUI fingerText;
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI prefabButtonText;
    public TextMeshProUGUI planeButtonText;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject[] raycastPrefabs;
    int prefabCount = 0;
    int currentPrefab = 0;

    private void Start()
    {
        prefabButtonText.text = raycastPrefabs[currentPrefab].name;
        prefabCountText.text = "Prefabs: 0";
    }

    void Update()
    {
        planeCountText.text = "Planes: " + planeManager.trackables.count;
        int screenFingers = Input.touchCount;
        fingerText.text = "Current touches: " + screenFingers.ToString();
        if (screenFingers > 0)
        {
            List<ARRaycastHit> raycastHits = new();
            Touch firstFinger = Input.GetTouch(0);
            if (firstFinger.phase == TouchPhase.Began)
                if (raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon))
                {
                    Instantiate(raycastManager.raycastPrefab, raycastHits[0].pose.position, Quaternion.identity);
                    prefabCount++;
                    prefabCountText.text = "Prefabs: " + prefabCount;
                }
            actionText.text = "Current action: " + firstFinger.phase.ToString();
        }
    }

    public void ChangePrefab()
    {
        currentPrefab++;
        if (currentPrefab == raycastPrefabs.Length)
            currentPrefab = 0;
        raycastManager.raycastPrefab = raycastPrefabs[currentPrefab];
        prefabButtonText.text = raycastPrefabs[currentPrefab].name;
    }

    public void ActivatePlaneManager()
    {
        if (planeManager.enabled)
        {
            foreach (ARPlane plane in planeManager.trackables)
                plane.gameObject.SetActive(false);
            planeManager.enabled = false;
            planeButtonText.text = "Enable planes";
        }
        else
        {
            foreach (ARPlane plane in planeManager.trackables)
                plane.gameObject.SetActive(true);
            planeManager.enabled = true;
            planeButtonText.text = "Disable planes";
        }
    }

    public void ResetButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}