using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Config;
using Assets.Scripts.Controllers.Characters.Enemies.Troll;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.ParameterObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The LevelManager is a subcompoment of the GameManager and is responsible for
    /// loading levels.
    /// It also loads and holds the GameObjects in a level that are of
    /// interest for the interaction logic.
    /// </summary>

    public class LevelManager : MonoBehaviour, TrollController.ITrollControllerListener, GoalController.IGoalControllerListener
    {

        private List<ILevelManagerListener> listeners;

        private GoalController goalController;
        private Level currentLevel;

        public LevelConfig CurrentLevelConfig { get; set; }

        public interface ILevelManagerListener
        {
            void OnLevelStarted(Level level);
        }

        public void Awake() 
        {
            
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
            CurrentLevelConfig = config;
            Application.LoadLevel(config.Name);
        }

        public void OnLevelWasLoaded(int level)
        {
            CurrentLevel = new Level()
            {
                CurrentLevelConfig = CurrentLevelConfig,
                MainCamera = GameObject.FindGameObjectWithTag(TagReferences.MainCamera),
                Obstacles = GameObject.FindGameObjectsWithTag(TagReferences.Obstacle).ToList(),
                Start = GameObject.FindWithTag(TagReferences.LevelStart),
                Goal = GameObject.FindWithTag(TagReferences.LevelGoal),
                Enemies = GameObject.FindGameObjectsWithTag(TagReferences.EnemyTroll).ToList()
            };
            RegisterListeners(); // TODO Move somewhere else
            foreach (var listener in listeners)
            {
                listener.OnLevelStarted(CurrentLevel);
            }
        
        }

        public Level CurrentLevel { get; set; }

        public void RegisterListeners()
        {
            RegisterEnemyListener();
            RegisterGoalListener();
        }

        private void RegisterEnemyListener()
        {
            CurrentLevel.Enemies.ForEach(x => x.GetComponent<TrollController>().RegisterListener(this));
        }

        private void RegisterGoalListener()
        {
            CurrentLevel.Goal.GetComponent<GoalController>().RegisterListener(this);
        }

        void TrollController.ITrollControllerListener.OnEnemyHurt(TrollController trollController)
        {
            trollController.UnregisterListener();
        }

        void GoalController.IGoalControllerListener.OnGoalReachedByImp()
        {
            Debug.Log("An imp has reached the goal.");
        }
    }
}