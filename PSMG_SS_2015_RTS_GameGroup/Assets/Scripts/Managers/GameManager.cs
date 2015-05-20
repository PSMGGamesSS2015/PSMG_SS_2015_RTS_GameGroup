using UnityEngine;
using System.Collections;

/// <summary>
/// The GameManager is a global object that glues the whole application together.
/// It holds references to the major components of the game and serves as event hub
/// for communication between these components.
/// Beyond that, the GameManager is a state machine that manages the game states, thereby
/// coordinating the flow of the application.
/// </summary>

public class GameManager : MonoBehaviour, LevelManager.LevelManagerListener, InputManager.InputManagerListener
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
        levelManager = GetComponent<LevelManager>();
        levelManager.RegisterListener(this);
        impManager = GetComponent<ImpManager>();
        uiManager = GetComponent<UIManager>();
        inputManager = GetComponent<InputManager>();
        inputManager.RegisterListener(this);

        gameState = GameState.NotStarted;

    }

    private void Start() {
        levelManager.LoadLevel(LevelConfig.LEVELS[0]);
    }

    private void Update()
    {
        if (gameState == GameState.LevelStarted)
        {
            impManager.SpawnImps();
        }
    }

    void LevelManager.LevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
    {
        impManager.SetLevelConfig(config, start);
        gameState = GameState.LevelStarted;
        //uiManager.SetButtonBar(lvl.ButtonBar);
    }
    
   void InputManager.InputManagerListener.DisplayImpLabels()
    {
        impManager.DisplayImpLabels();
    }
}