using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;


public class DisablePauseMenuPanel : MonoBehaviour {

    private GameObject pauseMenuPanel, gameManager;

	// Use this for initialization
	void Start () {            
        pauseMenuPanel = gameObject;
        gameManager = GameObject.Find("GameManager(Clone)");
        gameManager.GetComponent<InputManager>().PauseGame();


	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleActive();
        }
         * */
	
	}

    public void ToggleActive()
        {
            gameManager.GetComponent<InputManager>().PauseGame();

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
        