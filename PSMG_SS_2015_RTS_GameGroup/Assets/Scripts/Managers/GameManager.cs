using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The GameManager is a global object that glues the whole application together.
    /// It holds references to the major components of the game and sets up
    /// the communication between these components.
    /// </summary>

    public class GameManager : MonoBehaviour
    {
        private LevelManager levelManager;
        private ImpManager impManager;
        private UIManager uiManager;
        private InputManager inputManager;
        private SoundManager soundManager;
        private Physics2DManager physics2DManager;

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
        }

        private void InitManagers()
        {
            levelManager = GetComponent<LevelManager>();
            impManager = GetComponent<ImpManager>();
            uiManager = GetComponent<UIManager>();
            inputManager = GetComponent<InputManager>();
            soundManager = GetComponent<SoundManager>();
            physics2DManager = gameObject.AddComponent<Physics2DManager>();
        }

        private void SetupCollisionManagement()
        {
            physics2DManager.SetupCollisionManagement();
        }

        public void Start() 
        {
            SetupCommunicationBetweenManagers();

            levelManager.LoadLevel(LevelConfig.Levels[0]);
        }
        
        private void SetupCommunicationBetweenManagers()
        {
            levelManager.RegisterListener(impManager);
            levelManager.RegisterMenuSceneListener(impManager);
            levelManager.RegisterNarrativeSceneListener(impManager);
            levelManager.RegisterListener(soundManager);
            levelManager.RegisterMenuSceneListener(soundManager);
            levelManager.RegisterNarrativeSceneListener(soundManager);
            levelManager.RegisterListener(uiManager);
            levelManager.RegisterListener(inputManager);
            inputManager.RegisterListener(impManager);
            uiManager.RegisterListener(inputManager);
        }
    }
}