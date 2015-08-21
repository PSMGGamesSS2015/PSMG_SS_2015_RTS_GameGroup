using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;


public class DisablePauseMenuPanel : MonoBehaviour {

    private GameObject pauseMenuPanel, gameManager;

	// Use this for initialization
	void Start () {            
        pauseMenuPanel = gameObject;
        gameManager = GameObject.Find("GameManager(Clone)");
        Debug.Log("Disable Pause Menu Panel: Start (Pause)");

        gameManager.GetComponent<InputManager>().PauseGame();
	}
	

    public void ToggleActive()
        {
            Debug.Log("Disable Pause Menu Panel: Toggle Active (Pause)");

            gameManager.GetComponent<InputManager>().PauseGame();
            
            Debug.Log("ToggleActive wird ausgeführt activeSelf:");

            Debug.Log(pauseMenuPanel.activeSelf);
            if (pauseMenuPanel.activeSelf)
            {
                pauseMenuPanel.SetActive(false);
            }
            else
            {
                pauseMenuPanel.SetActive(true);
            }
                
        }
}
        