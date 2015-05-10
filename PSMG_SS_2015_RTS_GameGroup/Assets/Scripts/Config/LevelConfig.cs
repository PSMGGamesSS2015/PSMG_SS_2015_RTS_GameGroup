using UnityEngine;
using System.Collections;

/// <summary>
/// The LevelConfig class contains a static array of LevelConfigs that stores
/// basic information about the individual levels such as the number of imps
/// that can exist in the level at a time.
/// </summary>

public class LevelConfig  {

    private int maxImps;
    private float spawnInterval;
    private string name;
	
    public LevelConfig (int maxImps, float spawnInterval, string name) {
        this.maxImps = maxImps;
        this.spawnInterval = spawnInterval;
        this.name = name;
    }

    public int GetMaxImps()
    {
        return maxImps;
    }

    public float GetSpawnInterval()
    {
        return spawnInterval;
    }

    public string GetName()
    {
        return name;
    }

    public static LevelConfig[] LEVELS = 
    {                                 
        new LevelConfig(10, 4.0f, "Test Level")                            
    };
}
