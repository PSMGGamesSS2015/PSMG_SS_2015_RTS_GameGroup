using UnityEngine;
using System.Collections;

/*
 *  - manages the flow of the Application
 *  - major control unit that glues the whole application together
 */
public class GameManager : MonoBehaviour {

    // major components of the GameManager
    private LevelLoader levelLoader;
    private ImpManager impManager;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // Prevents the gameObject to which this script is attached from being disposed on scene load

        levelLoader = GetComponent<LevelLoader>(); // Initializing sub components
        impManager = GetComponent<ImpManager>();
    }

    void Update()
    {
        CheckForCompletion();
        SpawnImps();
    }

    void CheckForCompletion() {
        if (IsLevelLost()) {
            levelLoader.RestartLevel();
        } else if (IsLevelWon()) {
            levelLoader.LoadNextLevel();
        } 
    }

    private bool IsLevelLost()
    {
        return false;
    }

    private bool IsLevelWon()
    {
        return false;
    }

    private void SpawnImps()
    {
        impManager.SpawnImp();
    }

}
