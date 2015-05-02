using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehavior
{

    GameObject[] enemies;
    GameObject[] obstacles;
    GameObject start;
    GameObject end;

    List<Imp> imps;
    
    int maxImps;

    int levelID;
    int levelName;

    public void ReadLevelConfig(string level)
    {

    }

    public void SetObstacles(GameObject[] obstacles)
    {
        this.obstacles = obstacles;
    }

    public void SetEnd(GameObject end)
    {
        this.end = end;
    }

    public void SetEnemies(GameObject[] enemies)
    {
        this.enemies = enemies;
    }

    public void SetStart(GameObject start)
    {
        this.start = start;
    }

}