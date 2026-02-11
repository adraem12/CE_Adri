using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI planeCountText, prefabCountText, fingerText, actionText, prefabButtonText, planeButtonText;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject[] raycastPrefabs;
    int currentPrefab = 0;
    List<GameObject> prefabList = new();

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
                    GameObject newPrefab = Instantiate(raycastManager.raycastPrefab, raycastHits[0].pose.position, Quaternion.identity);
                    newPrefab.transform.LookAt(Camera.main.transform.position);
                    newPrefab.transform.eulerAngles = new Vector3(0, newPrefab.transform.eulerAngles.y, 0);
                    prefabList.Add(newPrefab);
                    prefabCountText.text = "Prefabs: " + prefabList.Count;
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
        foreach (GameObject prefab in prefabList)
            Destroy(prefab);
        prefabList.Clear();
        prefabCountText.text = "Prefabs: 0";
    }
}