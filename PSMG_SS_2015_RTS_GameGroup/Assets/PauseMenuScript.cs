using UnityEngine;
using Assets.Scripts.Managers;

public class PauseMenuScript : MonoBehaviour {
    GameObject DisablePauseMenuPanel;

    void Start()
    {
        DisablePauseMenuPanel = GameObject.Find("PauseMenu/PausePanel");
        DisablePauseMenuPanel.GetComponent<DisablePauseMenuPanel>().ToggleActive();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisablePauseMenuPanel.GetComponent<DisablePauseMenuPanel>().ToggleActive();
        }
    }

    public void ChangeToMainMenu()
    {
        DisablePauseMenuPanel.GetComponent<DisablePauseMenuPanel>().ToggleActive();
        LevelManager.Instance.LoadLevel(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeLevel(int sceneNumber)
    {
        LevelManager.Instance.LoadLevel(sceneNumber);
    }

}
