using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  The LevelManager is a subcomponent of the GameManager and
 *  is responsible for loading levels and managing the game logic.
 */

public class LevelManager : MonoBehaviour
{
    private Level lvl;
    private LevelConfig config = LevelConfig.LEVELS[0];

    public delegate void LevelStarted(Level lvl);
    public event LevelStarted OnLevelStarted;

    private void Awake()
    {
        LoadLevel(config.GetName());
    }

    private void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
        lvl = new Level(config);
    }

    
    private void OnLevelWasLoaded(int level)
    {
        lvl.RetrieveLevelData();
        OnLevelStarted(lvl);
    }

    
}