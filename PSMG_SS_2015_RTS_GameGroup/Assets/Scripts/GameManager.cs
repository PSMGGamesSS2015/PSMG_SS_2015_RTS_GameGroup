using UnityEngine;
using System.Collections;

/*
 *  The GameManager is a global object that glues the whole application together.
 *  It holds references to the major components of the game and serves as an event
 *  hub for communication between them.
 */

public class GameManager : MonoBehavior
{

    private LevelManager levelManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        levelManager = GetComponent<LevelManager>();
    }



}