using UnityEngine;
using System.Collections;

/*
 *  The GameManager is a global object that glues the whole application together.
 *  It holds references to the major components of the game and serves as an event
 *  hub for communication between them.
 */

public class GameManager : MonoBehaviour
{

    private const int STATE_LEVEL_STARTED = 0;
    private int gameState = -1;

    private LevelManager levelManager;
    private ImpManager impManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        levelManager = GetComponent<LevelManager>();
        impManager = GetComponent<ImpManager>();
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        levelManager.OnLevelStarted += OnLevelStarted;
    }

    private void OnLevelStarted(Level lvl)
    {
        impManager.SetLvl(lvl);
        gameState = STATE_LEVEL_STARTED;
    }

    private void Update()
    {
        if (gameState == STATE_LEVEL_STARTED)
        {
            impManager.SpawnImps();
        }
    }

}