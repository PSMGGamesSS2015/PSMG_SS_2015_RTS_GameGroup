using UnityEngine;
using System.Collections;

public class LevelConfig
{

    // TODO add number of professions allowed
    static int[] LEVEL01_maxProfessions = {1,1,1,1};
    
    public static const LevelConfig[] LEVEL_CONFIGS = 
    {  
        new LevelConfig(4, LEVEL01_maxProfessions)
    };

    private int maxImps;
    private int[] maxProfessions;

    public LevelConfig(int maxImps, int[] maxProfessions)
    {
        this.maxImps = maxImps;
        this.maxProfessions = maxProfessions;
    }

    public static LevelConfig NextLevelConfig()
    {
        return null;
    }

}