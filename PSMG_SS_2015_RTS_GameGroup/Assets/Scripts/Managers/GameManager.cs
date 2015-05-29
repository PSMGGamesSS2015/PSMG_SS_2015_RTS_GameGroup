using UnityEngine;
using System.Collections;

/// <summary>
/// The GameManager is a global object that glues the whole application together.
/// It holds references to the major components of the game and serves as event hub
/// for communication between these components.
/// Beyond that, the GameManager is a state machine that manages the game states, thereby
/// coordinating the flow of the application.
/// </summary>

public class GameManager : MonoBehaviour, LevelManager.LevelManagerListener
{
    private enum GameState
    {
        NotStarted,
        LevelStarted
    }

    private GameState gameState;

    private LevelManager levelManager;
    private ImpManager impManager;
    private UIManager uiManager;
    private InputManager inputManager;

    private void Awake()
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
    }

    private void Start() {
        SetupCommunicationBetweenManagers();
        levelManager.LoadLevel(LevelConfig.LEVELS[0]);
    }

    private void SetupCommunicationBetweenManagers()
    {
        levelManager.RegisterListener(this);
        levelManager.RegisterListener(impManager);
        inputManager.RegisterListener(impManager);
        //UIManager.RegisterListener(impManager);
    }

    private void Update()
    {
        if (gameState == GameState.LevelStarted)
        {
            impManager.SpawnImps();
        }
    }

    # region interface implementation

    void LevelManager.LevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
    {
        gameState = GameState.LevelStarted;
    }

    #endregion

}