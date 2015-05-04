using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  A class able to load and hold all GameObjects of a scene relevant for interaction logic.
 */

public class Level
{
    private LevelConfig config;
    
    private List<GameObject> obstacles;
    private List<GameObject> enemies;
    private GameObject start;
    private GameObject goal;

    public Level(LevelConfig config)
    {
        this.config = config;
        obstacles = new List<GameObject>();
        enemies = new List<GameObject>();
    }

    public LevelConfig GetConfig()
    {
        return config;
    }

    public GameObject GetStart()
    {
        return start;
    }

    public GameObject GetGoal()
    {
        return goal;
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public List<GameObject> GetObstacles()
    {
        return obstacles;
    }

    public Vector3 GetSpawnPosition()
    {
        return GetStart().transform.position;
    }

    public void RetrieveLevelData()
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