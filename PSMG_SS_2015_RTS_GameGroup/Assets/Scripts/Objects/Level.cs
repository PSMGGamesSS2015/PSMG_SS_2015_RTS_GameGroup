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

    public LevelConfig Config
    {
        get
        {
            return config;
        }
    }

    public GameObject Start
    {
        get 
        {
            return start;
        }
        
    }

    public GameObject Goal
    {
        get 
        {
            return goal;
        }
    }

    public List<GameObject> Enemies
    {
        get 
        {
            return enemies;
        }
    }

    public List<GameObject> Obstacles
    {
        get 
        {
            return obstacles;
        }
        
    }

    public Vector3 SpawnPosition
    {
        get 
        {
            return start.transform.position;
        }
    }

    public Button[] ButtonBar
    {
        get
        {
            return buttonBar;
        }
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