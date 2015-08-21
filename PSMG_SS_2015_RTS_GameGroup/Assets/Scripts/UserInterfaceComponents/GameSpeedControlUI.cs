using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;


public class GameSpeedControlUI : MonoBehaviour {

    private GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SlowGameSpeed()
    {
        gameManager.GetComponent<InputManager>().DecreaseGameSpeed();
    }

    public void pauseGame()
    {
        gameManager.GetComponent<InputManager>().PauseGame();
    }

    public void FastGameSpeed()
    {
        gameManager.GetComponent<InputManager>().IncreaseGameSpeed();
    }

}
