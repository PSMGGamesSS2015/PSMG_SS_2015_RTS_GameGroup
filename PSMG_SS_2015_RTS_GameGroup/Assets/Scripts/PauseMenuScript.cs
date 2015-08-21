using UnityEngine;
using Assets.Scripts.Managers;

public class PauseMenuScript : MonoBehaviour {
    GameObject DisablePauseMenuPanel, HelpMenuPanel;
    bool helpOpen=false;

    void Start()
    {
        DisablePauseMenuPanel = GameObject.Find("PauseMenu/PausePanel");
        HelpMenuPanel = GameObject.Find("PauseMenu/HelpPanel");
        DisablePauseMenuPanel.GetComponent<DisablePauseMenuPanel>().ToggleActive();
        HelpMenuPanel.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisablePauseMenuPanel.GetComponent<DisablePauseMenuPanel>().ToggleActive();
            if (helpOpen == true)
            {
                HelpMenuPanel.SetActive(false);
            }
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

    public void HelpBack()
    {
        Debug.Log("helpback wird ausgeführt ");
        helpOpen=false;
        HelpMenuPanel.SetActive(false);
    }

    public void ShowHelp()
    {
        Debug.Log("helpback wird ausgeführt DisablePauseMenuPanel.activeSelf:");
        Debug.Log(DisablePauseMenuPanel.activeSelf);
        HelpMenuPanel.SetActive(true);
        helpOpen=true;

    }

    public void ChangeLevel(int sceneNumber)
    {
        LevelManager.Instance.LoadLevel(sceneNumber);
    }

}
