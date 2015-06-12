using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Config;
using Assets.Scripts.Controllers.Characters;
using Assets.Scripts.Controllers.Objects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The LevelManager is a subcompoment of the GameManager and is responsible for
    /// loading levels.
    /// It also loads and holds the GameObjects in a level that are of
    /// interest for the interaction logic.
    /// </summary>

    public class LevelManager : MonoBehaviour, TrollController.IEnemyControllerListener, GoalController.IGoalControllerListener
    {

        private List<ILevelManagerListener> listeners;

        private LevelConfig currentLevelConfig;
        private List<GameObject> obstacles;
        private List<GameObject> enemies;
        private GameObject start;
        private GameObject goal;
        private GoalController goalController;

        public interface ILevelManagerListener
        {
            void OnLevelStarted(LevelConfig config, GameObject start);
        }

        public void Awake() 
        {
            obstacles = new List<GameObject>();
            enemies = new List<GameObject>();
            listeners = new List<ILevelManagerListener>();
            SetupCollisionManagement();
        }

        private void SetupCollisionManagement()
        {
            Physics2D.IgnoreLayerCollision(2, 2);
            Physics2D.IgnoreLayerCollision(2, 12);
        }

        public void RegisterListener(ILevelManagerListener listener)
        {
            listeners.Add(listener);
        }

        public void LoadLevel(LevelConfig config)
        {
            currentLevelConfig = config;
            Application.LoadLevel(config.Name);
        }

        public void OnLevelWasLoaded(int level)
        {
            RetrieveLevelData();
            foreach (ILevelManagerListener listener in listeners)
            {
                listener.OnLevelStarted(currentLevelConfig, start);
            }
        
        }

        public LevelConfig CurrentLevelConfig
        {
            get
            {
                return currentLevelConfig;
            }
        }

        public GameObject Start
        {
            get 
            {
                return start;
            }
        
        }

        public GameObject Goal
        {
            get 
            {
                return goal;
            }
        }

        public List<GameObject> Enemies
        {
            get 
            {
                return enemies;
            }
        }

        public List<GameObject> Obstacles
        {
            get 
            {
                return obstacles;
            }
        
        }

        public Vector3 SpawnPosition
        {
            get 
            {
                return start.transform.position;
            }
        }

        public void RetrieveLevelData()
        {
            RetrieveObstacles();
            RetrieveEnemies();
            RetrieveStart();
            RetrieveGoal();
        }

        private void RetrieveObstacles()
        {
            var obstacles = GameObject.FindGameObjectsWithTag(TagReferences.Obstacle);
            for (var i = 0; i < obstacles.Length; i++)
            {
                this.obstacles.Add(obstacles[i]);
            }
        }

        private void RetrieveEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagReferences.EnemyTroll);
            for (var i = 0; i < enemies.Length; i++)
            {
                this.enemies.Add(enemies[i]);
                enemies[i].GetComponent<TrollController>().RegisterListener(this);
            }
        }

        private void RetrieveStart()
        {
            start = GameObject.FindWithTag(TagReferences.LevelStart);
        }

        private void RetrieveGoal()
        {
            goal = GameObject.FindWithTag(TagReferences.LevelGoal);
            goalController = goal.GetComponent<GoalController>();
            goalController.RegisterListener(this);
        }

        void TrollController.IEnemyControllerListener.OnEnemyHurt(TrollController trollController)
        {
            trollController.UnregisterListener();
        }

        void GoalController.IGoalControllerListener.OnGoalReachedByImp()
        {
            Debug.Log("An imp has reached the goal.");
        }
    }
}