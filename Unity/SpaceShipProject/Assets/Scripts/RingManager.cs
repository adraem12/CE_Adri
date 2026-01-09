using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class RingManager : MonoBehaviour
{
    //Variables
    public static RingManager Instance;
    public GameObject ringPrefab;
    public GameObject coinSpawnerPrefab;
    public GameObject coinPrefab;
    public float spawnerQuantity = 5;
    Spline spline;
    List<BezierKnot> knotList;
    readonly List<GameObject> ringList = new();
    readonly List<GameObject> spawnerList = new();
    readonly List<GameObject> coinList = new();
    int currentRing = 0;

    private void Awake()
    {
        Instance = this;
        spline = GetComponent<SplineContainer>().Spline;
        knotList = spline.Knots.ToList();
    }

    public void StartGame()
    {
        for (int i = 0; i < knotList.Count; i++) //Añade un anillo por nudo del spline
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
        SpawnCoins();
    }

    void Spawner(int start, int end)
    {
        for (int j = 1; j <= spawnerQuantity; j++) //Spawnea las posibles posiciones de las monedas
        {
            SplineUtility.GetNearestPoint(spline, Vector3.Lerp(knotList[start].Position, knotList[end].Position, 1f / 7f * j), out float3 finalPoint, out _);
            GameObject newSpawner = Instantiate(coinSpawnerPrefab, finalPoint * Random.Range(0.975f, 1.025f), Quaternion.identity, transform.GetChild(1));
            newSpawner.name = "CoinSpawner" + start + "" + end + "-" + (j - 1);
            spawnerList.Add(newSpawner);
        }
        ringList[0].GetComponent<MeshRenderer>().enabled = true; //Inicializa el primer anillo
        ringList[0].GetComponent<MeshCollider>().enabled = true;
        ringList[0].GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void SpawnCoins()
    {
        List<int> indexedCoins = new();
        int index;
        for (int i = 0; i < 15; i++) //Instancia monedas de forma aleatoria sin repetir posición
        {
            do
                index = Random.Range(0, 26); //Hardcodeado
            while (indexedCoins.Contains(index));
            indexedCoins.Add(index);
            coinList.Add(Instantiate(coinPrefab, SpawnerList().ToArray()[index].transform.position, Quaternion.identity, transform.GetChild(2)));
        }
        for (int i = 0; i < 5; i++)
        {
            do
                index = Random.Range(26, 41); //Hardcodeado
            while (indexedCoins.Contains(index));
            indexedCoins.Add(index);
            coinList.Add(Instantiate(coinPrefab, SpawnerList().ToArray()[index].transform.position, Quaternion.identity, transform.GetChild(2)));
        }
    }

    public void NextRing()
    {
        //Desactiva el anillo activado y activa el siguiente
        ringList[currentRing].GetComponent<MeshRenderer>().material.color = Color.red;
        ringList[currentRing].GetComponent<MeshCollider>().enabled = false;
        currentRing++;
        if (currentRing < ringList.Count)
        {
            ringList[currentRing].GetComponent<MeshRenderer>().enabled = true;
            ringList[currentRing].GetComponent<MeshCollider>().enabled = true;
            ringList[currentRing].GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
            GameManager.Instance.GameOver(true);
    }

    public void ResetGame()
    {
        foreach (GameObject ring in ringList)
            Destroy(ring);
        foreach (GameObject spawner in spawnerList)
            Destroy(spawner);
        foreach (GameObject coin in coinList)
            Destroy(coin);
        ringList.Clear();
        spawnerList.Clear();
        currentRing = 0;
    }

    public List<GameObject> SpawnerList()
    {
        return spawnerList;
    }

    public List<GameObject> RingList()
    {
        return ringList;
    }
}