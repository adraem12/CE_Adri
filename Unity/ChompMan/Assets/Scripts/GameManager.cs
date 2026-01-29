using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("SceneObjects")]
    public static GameManager instance;
    public GameObject player;
    public GameObject enemySpawnParent;
    public GameObject cherrySpawnParent;
    public GameObject dotsParent;
    public GameObject cherry;
    public GameObject smallGhost;
    public GameObject bigGhost;
    public Controls controls;
    public static int enemiesLeft;
    public static int enemiesKilled;
    public static int dotsLeft;
    public static bool cherryState;

    private void Awake()
    {
        enemiesLeft = 0;
        enemiesKilled = 0;
        dotsLeft = dotsParent.transform.childCount;
        instance = this;
        controls = new();
        controls.Enable();
        StartCoroutine(EnemySpawner());
        Instantiate(cherry, cherrySpawnParent.transform.GetChild(Random.Range(0, cherrySpawnParent.transform.childCount)).position, Quaternion.identity);
    }

    public void GameOver(bool win)
    {
        StopAllCoroutines();
        controls.Disable();
        UIManager.Instance.GameOver(win);
        foreach (EnemyController enemy in FindObjectsByType<EnemyController>(FindObjectsSortMode.None))
            Destroy(enemy.gameObject);
    }

    IEnumerator EnemySpawner()
    {
        int spawnSmall, spawnBig;
        while (true) 
        { 
            spawnSmall = Random.Range(0, enemySpawnParent.transform.childCount);
            spawnBig = spawnSmall;
            while (spawnBig == spawnSmall)
                spawnBig = Random.Range(0, enemySpawnParent.transform.childCount);
            Instantiate(smallGhost, enemySpawnParent.transform.GetChild(spawnSmall).position, Quaternion.identity);
            Instantiate(bigGhost, enemySpawnParent.transform.GetChild(spawnBig).position, Quaternion.identity);
            enemiesLeft += 2;
            UIManager.Instance.UpdateEnemiesText();
            yield return new WaitForSeconds(10);
        }
    }

    public IEnumerator CherryState()
    {
        StopCoroutine(EnemySpawner());
        cherryState = true;
        foreach (EnemyController enemy in FindObjectsByType<EnemyController>(FindObjectsSortMode.None))
        {
            enemy.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            enemy.GetComponent<NavMeshAgent>().speed *= 0.5f;
            enemy.GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
        }
        yield return new WaitForSeconds(10);
        foreach (EnemyController enemy in FindObjectsByType<EnemyController>(FindObjectsSortMode.None))
        {
            enemy.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            enemy.GetComponent<NavMeshAgent>().speed *= 2f; 
        }
        StartCoroutine(EnemySpawner());
        cherryState = false;
    }
}