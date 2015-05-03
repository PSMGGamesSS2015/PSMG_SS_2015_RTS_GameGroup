using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehavior
{

    public delegate void LevelLoaded(Level level);
    public event LevelLoaded OnLevelLoaded;

    Level currentLevel;

    void Awake()
    {
        LoadLevel("Main menu");
    }

    void Update()
    {
        if (!IsLevelLoaded())
        {
            LoadLevel("Main menu");
        }

    }

    void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
        RetrieveLevelData();
        OnLevelLoaded(currentLevel);
    }

    void RetrieveLevelData()
    {
        currentLevel = new Level();

        RetrieveObstacles(currentLevel);
        RetrieveEnemies(currentLevel);
        RetrieveStart(currentLevel);
        RetrieveEnd(currentLevel);
    }

    private void RetrieveObstacles(Level level)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag<Obstacle>();
        level.SetObstacles(obstacles);
    }

    private void RetrieveEnemies(Level level)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag<Enemy>();
        level.SetEnemies(enemies);
    }

    private void RetrieveStart(Level level)
    {
        GameObject start = GameObject.FindWithTag<Start>();
        level.SetStart(start);
    }

    private void RetrieveEnd(Level level)
    {
        GameObject end = GameObject.FindWithTag<End>();
        level.SetEnd(end); 
    }


    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(LevelConfig.NextLevelConfig());
    }

    public bool IsLevelLoaded()
    {
        if (Application.loadedLevel != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}