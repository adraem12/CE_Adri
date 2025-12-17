using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class SplineInstancesScript : MonoBehaviour
{
    public GameObject ringPrefab;
    public GameObject coinSpawnerPrefab;
    public float spawnerQuantity = 5;
    Spline spline;
    List<BezierKnot> knotList;
    readonly List<GameObject> ringList = new();
    readonly List<GameObject> spawnerList = new();

    private void Awake()
    {
        spline = GetComponent<SplineContainer>().Spline;
        knotList = spline.Knots.ToList();
        for (int i = 0; i < knotList.Count; i++)
        {
            GameObject newRing = Instantiate(ringPrefab, knotList[i].Position, knotList[i].Rotation, transform.GetChild(0));
            newRing.name = "Ring" + i;
            ringList.Add(newRing);
            if (i > 0)
            {
                Spawner(i - 1, i);
                if (i == knotList.Count - 1)
                    Spawner(i, 0);
            }
        }
    }

    void Spawner(int start, int end)
    {
        for (int j = 1; j <= spawnerQuantity; j++)
        {
            SplineUtility.GetNearestPoint(spline, Vector3.Lerp(knotList[start].Position, knotList[end].Position, 1f / 7f * j), out float3 finalPoint, out _);
            GameObject newSpawner = Instantiate(coinSpawnerPrefab, finalPoint * Random.Range(0.975f, 1.025f), Quaternion.identity, transform.GetChild(1));
            newSpawner.name = "CoinSpawner" + (start) + "" + end + "-" + (j - 1);
            spawnerList.Add(newSpawner);
        }
    }

    public List<GameObject> SpawnerList()
    {
        return spawnerList;
    }
}