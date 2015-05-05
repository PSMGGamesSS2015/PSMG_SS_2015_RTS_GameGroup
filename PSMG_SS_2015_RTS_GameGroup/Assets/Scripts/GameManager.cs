using UnityEngine;
using System.Collections;

/*
 *  The GameManager is a global object that glues the whole application together.
 *  It holds references to the major components of the game and serves as an event
 *  hub for communication between them.
 */

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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        levelManager = GetComponent<LevelManager>();
        levelManager.RegisterListener(this);
        impManager = GetComponent<ImpManager>();
        gameState = GameState.NotStarted;
    }

    private void Update()
    {
        if (gameState == GameState.LevelStarted)
        {
            impManager.SpawnImps();
        }
    }

    void LevelManager.LevelManagerListener.OnLevelStarted(Level lvl)
    {
        impManager.SetLvl(lvl);
        gameState = GameState.LevelStarted;
    }
}