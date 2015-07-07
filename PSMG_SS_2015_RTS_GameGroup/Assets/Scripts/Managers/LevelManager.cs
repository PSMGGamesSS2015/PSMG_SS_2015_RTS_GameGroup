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
    ///     The LevelManager is a subcompoment of the GameManager and is responsible for
    ///     loading levels.
    ///     It also loads and holds the GameObjects in a level that are of
    ///     interest for the interaction logic.
    /// </summary>
    public class LevelManager : MonoBehaviour, TrollController.ITrollControllerListener,
        GoalController.IGoalControllerListener
    {
        public static LevelManager Instance;
        private List<ILevelManagerListener> listeners;
        public LevelConfig CurrentLevelConfig { get; set; }
        public Level CurrentLevel { get; set; }

        void GoalController.IGoalControllerListener.OnGoalReachedByImp()
        {
            Debug.Log("An imp has reached the goal.");
        }

        void TrollController.ITrollControllerListener.OnEnemyHurt(TrollController trollController)
        {
            trollController.UnregisterListener();
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

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
            switch (CurrentLevelConfig.Type)
            {
                case LevelConfig.LevelType.InGame:
                    LoadInGameLevel();
                    break;
                case LevelConfig.LevelType.Menu:
                    LoadMenuLevel();
                    break;
                case LevelConfig.LevelType.Narrative:
                    LoadNarrativeLevel();
                    break;
            }
        }

        private void LoadNarrativeLevel()
        {
            // TODO
        }

        private void LoadMenuLevel()
        {
            // TODO 
        }

        public void LoadInGameLevel()
        {
            CurrentLevel = new Level
            {
                CurrentLevelConfig = CurrentLevelConfig,
                MainCamera = GameObject.FindGameObjectWithTag(TagReferences.MainCamera),
                LeftMargin = GameObject.FindGameObjectWithTag(TagReferences.LeftMargin),
                RightMargin = GameObject.FindGameObjectWithTag(TagReferences.RightMargin),
                Obstacles = GameObject.FindGameObjectsWithTag(TagReferences.Obstacle).ToList(),
                Start = GameObject.FindWithTag(TagReferences.LevelStart),
                Goal = GameObject.FindWithTag(TagReferences.LevelGoal),
                Enemies = GameObject.FindGameObjectsWithTag(TagReferences.EnemyTroll).ToList()
            };
            RegisterListeners(); // TODO Move somewhere else
            listeners.ForEach(l => l.OnLevelStarted(CurrentLevel));
        }

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

        public interface ILevelManagerListener
        {
            void OnLevelStarted(Level level);
        }
    }

}