using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("SceneObjects")]
    public GameObject player;
    public GameObject enemySpawnParent, cherrySpawnParent, dotPositionsParent, dotsParent;
    [Header("Prefabs")]
    public GameObject cherryPrefab;
    public GameObject smallGhost, bigGhost, dotPrefab;
    public Controls controls;
    DifficultyStats difficultyStats;
    int objectSpawnTime, enemySpawnTime;
    int objectSpawnDistance, enemySpawnDistance;
    public static int enemiesLeft, enemiesKilled, dotsLeft = 0;
    public static bool cherryState;
    public static float timer;
    Transform[] dotPositions;
    int lastDifficulty = 0;

    private void Awake()
    {
        instance = this;
        controls = new();
        difficultyStats = GetComponent<DifficultyStats>();
        dotPositions = dotPositionsParent.GetComponentsInChildren<Transform>();
    }

    public void StartNewGame(int difficulty)
    {
        controls.Enable();
        enemiesLeft = 0;
        enemiesKilled = 0;
        timer = 0;
        if (difficulty == -1)
            difficulty = lastDifficulty;
        else
            lastDifficulty = difficulty;
        SetDifficulty(difficulty);
        DotGenerator(100);
        StartCoroutine(EnemySpawner());
        ChooseSpawn(cherrySpawnParent.transform, objectSpawnDistance, 100, out int index);
        Instantiate(cherryPrefab, cherrySpawnParent.transform.GetChild(index).position, Quaternion.identity);
    }

    private void Update()
    {
        timer += Time.deltaTime;
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
        while (true) 
        { 
            ChooseSpawn(enemySpawnParent.transform, enemySpawnDistance, enemySpawnDistance * 2, out int smallIndex);
            ChooseSpawn(enemySpawnParent.transform, enemySpawnDistance, enemySpawnDistance * 2, out int bigIndex);
            bigIndex = smallIndex;
            while (bigIndex == smallIndex)
                bigIndex = Random.Range(0, enemySpawnParent.transform.childCount);
            Instantiate(smallGhost, enemySpawnParent.transform.GetChild(smallIndex).position, Quaternion.identity);
            Instantiate(bigGhost, enemySpawnParent.transform.GetChild(bigIndex).position, Quaternion.identity);
            enemiesLeft += 2;
            UIManager.Instance.UpdateEnemiesText();
            yield return new WaitForSeconds(enemySpawnTime);
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
        yield return new WaitForSeconds(objectSpawnTime);
        foreach (EnemyController enemy in FindObjectsByType<EnemyController>(FindObjectsSortMode.None))
        {
            enemy.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
            enemy.GetComponent<NavMeshAgent>().speed *= 2f;
        }
        StartCoroutine(EnemySpawner());
        cherryState = false;
        yield return new WaitForSeconds(objectSpawnTime);
        ChooseSpawn(cherrySpawnParent.transform, objectSpawnDistance, 100, out int index);
        Instantiate(cherryPrefab, cherrySpawnParent.transform.GetChild(index).position, Quaternion.identity);
    }

    void DotGenerator(int quantity)
    {
        if (dotsLeft != 0)
            foreach (DotScript dot in dotsParent.GetComponentsInChildren<DotScript>())
                Destroy(dot.gameObject);
        List<int> positions = new();
        for (int i = 0; i < quantity; i++)
        {
            int position = Random.Range(0, dotPositions.Length);
            while (positions.Contains(position))
                position = Random.Range(0, dotPositions.Length);
            GameObject newDot = Instantiate(dotPrefab, dotPositions[position].position, Quaternion.identity);
            newDot.transform.parent = dotsParent.transform;
            dotsLeft++;
        }
        UIManager.Instance.UpdateDotsText();
    }

    void SetDifficulty(int difficulty)
    {
        if (difficulty == 0)
        {
            player.GetComponent<NavPlayer>().speed = difficultyStats.playerSpeed.x;
            smallGhost.GetComponent<NavMeshAgent>().speed = difficultyStats.smallSpeed.x;
            bigGhost.GetComponent<NavMeshAgent>().speed = difficultyStats.bigSpeed.x;
            objectSpawnTime = (int)difficultyStats.objectSpawnTime.x;
            enemySpawnTime = (int)difficultyStats.enemySpawnTime.x;
            objectSpawnDistance = (int)difficultyStats.objectSpawnDistance.x;
            enemySpawnDistance = (int)difficultyStats.enemySpawnDistance.x;
        }
        else
        {
            player.GetComponent<NavPlayer>().speed = difficultyStats.playerSpeed.y;
            smallGhost.GetComponent<NavMeshAgent>().speed = difficultyStats.smallSpeed.y;
            bigGhost.GetComponent<NavMeshAgent>().speed = difficultyStats.bigSpeed.y;
            objectSpawnTime = (int)difficultyStats.objectSpawnTime.y;
            enemySpawnTime = (int)difficultyStats.enemySpawnTime.y;
            objectSpawnDistance = (int)difficultyStats.objectSpawnDistance.y;
            enemySpawnDistance = (int)difficultyStats.enemySpawnDistance.y;
        }
    }

    void ChooseSpawn(Transform parent, int minDistance, int maxDistance, out int index)
    {
        float distance = -1;
        index = 0;
        while (distance < minDistance || distance > maxDistance)
        {
            index = Random.Range(0, cherrySpawnParent.transform.childCount);
            distance = Vector3.Distance(parent.GetChild(index).position, player.transform.position);
        }
    }
}