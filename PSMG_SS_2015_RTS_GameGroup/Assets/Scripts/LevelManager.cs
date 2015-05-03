using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  The LevelManager is a subcomponent of the GameManager and
 *  is responsible for loading levels and managing the game logic.
 */

public class LevelManager : MonoBehaviour
{

    private const int MAX_IMPS = 1;
    private const float SPAWNING_INTERVALL = 4.0f; // in seconds
    private const string TEST_LEVEL = "Test Level";

    public GameObject impPrefab;
    private float spawnCounter;
    private int currentImps;

    // components in a level
    private List<GameObject> obstacles;
    private List<GameObject> enemies;
    private GameObject start;
    private GameObject goal;

    private void Awake()
    {
        obstacles = new List<GameObject>();
        enemies = new List<GameObject>();
        LoadLevel(TEST_LEVEL);
    }

    private void Update()
    {
        SpawnImps();
    }

    private void SpawnImps()
    {
        if (currentImps == 0)
        {
            SpawnImp();
            currentImps++;
            spawnCounter = 0f;
        }
        else if (currentImps < MAX_IMPS && spawnCounter >= SPAWNING_INTERVALL)
        {
            SpawnImp();
            currentImps++;
            spawnCounter = 0f;
        }
        spawnCounter += Time.deltaTime;
        //Debug.Log(spawnCounter);
    }

    private void SpawnImp()
    {
        Instantiate(impPrefab, new Vector3(-3f, -1f, 0f), Quaternion.identity);
    }

    private void LoadLevel(string levelName)
    {
        Reset();
        Application.LoadLevel(levelName);
        RetrieveLevelData();
    }

    private void Reset()
    {
        obstacles.Clear();
        enemies.Clear();
        start = null;
        goal = null;
        currentImps = 0;
    }

    private void RetrieveLevelData()
    {
        RetrieveObstacles();
        RetrieveEnemies();
        RetrieveStart();
        RetrieveGoal();
    }

    private void RetrieveObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            this.obstacles.Add(obstacles[i]);
        }
    }

    private void RetrieveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            this.enemies.Add(enemies[i]);
        }
    }

    private void RetrieveStart()
    {
        start = GameObject.FindWithTag("Start");
    }

    private void RetrieveGoal()
    {
        goal = GameObject.FindWithTag("Goal");
    }
}