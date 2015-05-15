using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The LevelManager is a subcompoment of the GameManager and is responsible for
/// loading levels.
/// It also loads and holds the GameObjects in a level that are of
/// interest for the interaction logic.
/// </summary>

public class LevelManager : MonoBehaviour, EnemyController.EnemyControllerListener
{

    private LevelManagerListener listener;

    private LevelConfig currentLevelConfig;

    private List<GameObject> obstacles;
    private List<GameObject> enemies;
    private GameObject start;
    private GameObject goal;

    public interface LevelManagerListener
    {
        void OnLevelStarted(LevelConfig config, GameObject start);
    }

    private void Awake() {
        
        obstacles = new List<GameObject>();
        enemies = new List<GameObject>();
    }

    public void RegisterListener(LevelManagerListener listener)
    {
        this.listener = listener;
    }

    public void LoadLevel(LevelConfig config)
    {
        currentLevelConfig = config;
        Application.LoadLevel(config.Name);
    }

    private void OnLevelWasLoaded(int level)
    {
        RetrieveLevelData();
        listener.OnLevelStarted(currentLevelConfig, start);
    }

    public LevelConfig CurrentLevelConfig
    {
        get
        {
            return currentLevelConfig;
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
            enemies[i].GetComponent<EnemyController>().RegisterListener(this);
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

    void EnemyController.EnemyControllerListener.OnEnemyHurt(EnemyController enemyController)
    {
        enemyController.UnregisterListener();
    }
}