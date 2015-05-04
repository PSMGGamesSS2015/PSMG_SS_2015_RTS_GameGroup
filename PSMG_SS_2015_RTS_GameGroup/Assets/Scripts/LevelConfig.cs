using UnityEngine;
using System.Collections;

/*
 *  Contains configurations for all levels in the game.
 */

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
        new LevelConfig(1, 4.0f, "Test Level")                            
    };
}
