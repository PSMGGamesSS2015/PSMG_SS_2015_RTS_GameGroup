using Assets.Scripts.AssetReferences;
using Assets.Scripts.Config;
using Assets.Scripts.ParameterObjects;
using Assets.Scripts.UserInterfaceComponents;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The GameManager is a global object that glues the whole application together.
    /// It holds references to the major components of the game and serves as event hub
    /// for communication between these components.
    /// Beyond that, the GameManager is a state machine that manages the game states, thereby
    /// coordinating the flow of the application.
    /// </summary>

    public class GameManager : MonoBehaviour, LevelManager.ILevelManagerListener, UIManager.IUIManagerListener
    {
        public enum GameState
        {
            NotStarted,
            InGameLevelStarted
        }

        public GameState State;

        private LevelManager levelManager;
        private ImpManager impManager;
        private UIManager uiManager;
        private InputManager inputManager;
        private SoundManager soundManager;
        private Physics2DManager physics2DManager;
        // ReSharper disable once NotAccessedField.Local
        private SpecialEffectsManager specialEffectsManager;

        public static GameManager Instance;

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

            DontDestroyOnLoad(gameObject);
            InitManagers();
            SetupCollisionManagement();
            State = GameState.NotStarted;
        }

        private void SetupCollisionManagement()
        {
            physics2DManager.SetupCollisionManagement();
        }

        private void InitManagers()
        {
            levelManager = GetComponent<LevelManager>();
            impManager = GetComponent<ImpManager>();
            uiManager = GetComponent<UIManager>();
            inputManager = GetComponent<InputManager>();
            soundManager = GetComponent<SoundManager>();
            specialEffectsManager = GetComponent<SpecialEffectsManager>();
            physics2DManager = gameObject.AddComponent<Physics2DManager>();
        }

        public void Start() 
        {
            SetupCommunicationBetweenManagers();
            levelManager.LoadLevel(LevelConfig.Levels[0]);
        }
        
        private void SetupCommunicationBetweenManagers()
        {
            levelManager.RegisterListener(this);
            levelManager.RegisterListener(impManager);
            levelManager.RegisterListener(soundManager);
            levelManager.RegisterMenuSceneListener(soundManager);
            levelManager.RegisterNarrativeSceneListener(soundManager);
            levelManager.RegisterListener(uiManager);
            levelManager.RegisterListener(inputManager);
            inputManager.RegisterListener(impManager);
            uiManager.RegisterListener(inputManager);
            uiManager.RegisterListener(this);
        }

        public void Update()
        {
            if (State == GameState.InGameLevelStarted) impManager.SpawnImps();
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            State = GameState.InGameLevelStarted;
        }

        // The following parts need to be placed here to avoid a circular base class reference.

        private UserInterface currentUserInterface;

        void UIManager.IUIManagerListener.OnUserInterfaceLoaded(UserInterface userInterface)
        {
            if (currentUserInterface != null) impManager.UnregisterListener(currentUserInterface);
            currentUserInterface = userInterface;
            impManager.RegisterListener(userInterface);
        }
    }
}