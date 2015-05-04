using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The LevelManager is a subcompoment of the GameManager and is responsible for
/// loading levels and managing the game logic.
/// </summary>

public class LevelManager : MonoBehaviour
{
    private Level lvl;

    public delegate void LevelStarted(Level lvl);
    public event LevelStarted OnLevelStarted;

    public void LoadLevel(LevelConfig config)
    {
        Application.LoadLevel(config.GetName());
        lvl = new Level(config);
    }
    
    private void OnLevelWasLoaded(int level)
    {
        lvl.RetrieveLevelData();
        OnLevelStarted(lvl);
    }

    
}