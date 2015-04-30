using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private LevelManager levelManager;

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

        DontDestroyOnLoad(gameObject);
        levelManager = GetComponent<LevelManager>();
        InitGame();
    }

    private void InitGame()
    {
        // TODO
    }
    
}
