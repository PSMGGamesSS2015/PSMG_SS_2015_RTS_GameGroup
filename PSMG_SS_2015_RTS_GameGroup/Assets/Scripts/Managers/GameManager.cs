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
        private enum GameState
        {
            NotStarted,
            LevelStarted
            // TODO
        }

        private GameState gameState;

        private LevelManager levelManager;
        private ImpManager impManager;
        private UIManager uiManager;
        private InputManager inputManager;
        private SoundManager soundManager;
        private PersistenceManager persistenceManager;

        // TODO Move elsewhere
        private UserInterface currentUserInterface;

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
            gameState = GameState.NotStarted;
        }

        private void InitManagers()
        {
            levelManager = GetComponent<LevelManager>();
            impManager = GetComponent<ImpManager>();
            uiManager = GetComponent<UIManager>();
            inputManager = GetComponent<InputManager>();
            soundManager = GetComponent<SoundManager>();
            persistenceManager = GetComponent<PersistenceManager>();
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
            levelManager.RegisterListener(uiManager);
            levelManager.RegisterListener(inputManager);
            inputManager.RegisterListener(impManager);
            uiManager.RegisterListener(inputManager);
            uiManager.RegisterListener(this);
        }

        public void Update()
        {
            if (gameState == GameState.LevelStarted)
            {
                impManager.SpawnImps();
            }
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            gameState = GameState.LevelStarted;
        }

        void UIManager.IUIManagerListener.OnUserInterfaceLoaded(UserInterface userInterface)
        {
            if (currentUserInterface != null)
            {
                impManager.UnregisterListener(currentUserInterface);
            }
            currentUserInterface = userInterface;
            impManager.RegisterListener(userInterface);
        }
    }
}