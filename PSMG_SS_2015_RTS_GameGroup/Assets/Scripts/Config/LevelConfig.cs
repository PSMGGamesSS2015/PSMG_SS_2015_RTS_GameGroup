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
    private int[] maxProfessions;

    public LevelConfig (int maxImps, float spawnInterval, string name, int[] maxProfessions) {
        this.maxImps = maxImps;
        this.spawnInterval = spawnInterval;
        this.name = name;
        this.maxProfessions = maxProfessions;
    }

    #region properties

    public int MaxImps
    {
        get
        {
            return maxImps;
        }
    }

    public float SpawnInterval
    {
        get
        {
            return spawnInterval;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public int[] MaxProfessions
    {
        get
        {
            return maxProfessions;
        }
    }

    #endregion

    /// <summary>
    /// This is a globally usable array of level configurations.
    /// </summary>
    public static LevelConfig[] LEVELS = 
    {                                 
        new LevelConfig(4, 4.0f, SceneReferences.TEST_SCENE, new int[8] {4,3,3,3,0,0,0,0})                         
    };
}
