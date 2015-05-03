using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  The LevelManager is a subcomponent of the GameManager and
 *  is responsible for loading levels and managing the game logic.
 */

public class LevelManager : MonoBehavior
{

    private const int MAX_IMPS = 4;
    private const float SPAWNING_INTERVALL = 4.0f; // in seconds

    public GameObject impPrefab;
    private float spawnCounter;
    private int currentImps;

    // components in a level
    private List<GameObject> obstacles;
    private List<GameObject> enemies;
    private GameObject start;
    private GameObject end;

    private void Awake()
    {
        obstacles = new List<GameObject>();
        enemies = new List<GameObject>();
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
            spawnCounter = 0f;
        }
        else if (currentImps < MAX_IMPS && spawnCounter >= SPAWNING_INTERVALL)
        {
            SpawnImp();
            spawnCounter = 0f;
        }
        spawnCounter += Time.deltaTime;
        Debug.Log(spawnCounter);
    }

    private void SpawnImp()
    {
        Instantiate(impPrefab);
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
        end = null;
        currentImps = 0;
    }

    private void RetrieveLevelData()
    {
        RetrieveObstacles();
        RetrieveEnemies();
        RetrieveStart();
        RetrieveEnd();
    }

    private void RetrieveObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag<Obstacle>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            this.obstacles.Add(obstacles[i]);
        }
    }

    private void RetrieveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameOjectsWithTag<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            this.enemies.Add(enemies[i]);
        }
    }

    private void RetrieveStart()
    {
        start = GameObject.FindWithTag<Start>();
    }

    private void RetrieveEnd()
    {
        end = GameObject.FindWithTag<End>();
    }
}