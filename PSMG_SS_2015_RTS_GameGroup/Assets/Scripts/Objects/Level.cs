using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Level class loads and holds the GameObjects in a level that are of
/// interest for the interaction logic.
/// </summary>

public class Level
{
    private LevelConfig config;
    
    private List<GameObject> obstacles;
    private List<GameObject> enemies;
    private GameObject start;
    private GameObject goal;
    private Button[] buttonBar;

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

    public Button[] GetButtonBar()
    {
        return buttonBar;
    }

    public void RetrieveLevelData()
    {
        RetrieveObstacles();
        RetrieveEnemies();
        RetrieveStart();
        RetrieveGoal();
        RetrieveButtonBar();
    }

    private void RetrieveButtonBar() // TODO rework
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        Button[] newButtonBar = new Button[buttons.Length];

        for (int i = 0; i < newButtonBar.Length; i++)
        {
            newButtonBar[i] = buttons[i].GetComponent<Button>();
            Debug.Log(newButtonBar[i]);
        }

        buttonBar = newButtonBar;
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