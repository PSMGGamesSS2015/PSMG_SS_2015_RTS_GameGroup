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
            HighlightableObjects = new List<GameObject>();
        }

        public LevelConfig CurrentLevelConfig { get; set; }

        public GameObject Start { get; set; }

        public GameObject Goal { get; set; }

        public GameObject MainCamera { get; set; }

        public GameObject LeftMargin { get; set; }

        public GameObject RightMargin { get; set; }

        public List<GameObject> Enemies { get; set; }

        // TODO is this really needed?
        public List<GameObject> Obstacles { get; set; }

        public List<GameObject> HighlightableObjects; 

        public Vector3 SpawnPosition
        {
            get
            {
                return Start.transform.position;
            }
        }

        public GameObject TopMargin { get; set; }
        public GameObject BottomMargin { get; set; }
        public List<GameObject> CheckPoints { get; set; }
    }
}
