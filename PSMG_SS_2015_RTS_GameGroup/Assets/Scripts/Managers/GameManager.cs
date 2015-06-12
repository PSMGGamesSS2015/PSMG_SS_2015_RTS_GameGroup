using Assets.Scripts.Config;
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
        private UserInterface.UserInterface currentUserInterface;

        public void Awake()
        {
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
            impManager.SoundMgr = soundManager;
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

        # region interface implementation

        void LevelManager.ILevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
        {
            gameState = GameState.LevelStarted;
        }

        #endregion

        void UIManager.IUIManagerListener.OnUserInterfaceLoaded(UserInterface.UserInterface userInterface)
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