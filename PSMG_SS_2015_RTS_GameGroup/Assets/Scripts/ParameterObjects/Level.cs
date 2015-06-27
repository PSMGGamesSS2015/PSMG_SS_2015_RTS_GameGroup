using System.Collections.Generic;
using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts.ParameterObjects
{
    public class Level
    {

        public Level()
        {
            Obstacles = new List<GameObject>();
            Enemies = new List<GameObject>();
        }

        public LevelConfig CurrentLevelConfig { get; set; }

        public GameObject Start { get; set; }

        public GameObject Goal { get; set; }

        public GameObject MainCamera { get; set; }

        public List<GameObject> Enemies { get; set; }

        public List<GameObject> Obstacles { get; set; }

        public Vector3 SpawnPosition
        {
            get
            {
                return Start.transform.position;
            }
        }
    }
}
