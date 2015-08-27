using UnityEngine;
using Assets.Scripts.Managers;

public class PauseMenuScript : MonoBehaviour {
    GameObject PauseMenuPanel, HelpMenuPanel, gameManager, WinningScreenPanel;
    bool pauseOpen = false;
    bool helpOpen = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager(Clone)");
        PauseMenuPanel = GameObject.Find("UserInterface(Clone)/PauseMenu/PausePanel");
        HelpMenuPanel = GameObject.Find("UserInterface(Clone)/PauseMenu/HelpPanel");
        WinningScreenPanel = GameObject.Find("UserInterface(Clone)/WinningScreen");
        Debug.Log(WinningScreenPanel);
        WinningScreenPanel.SetActive(false);
        HelpMenuPanel.SetActive(false);
        PauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC wurde geklickt");

            if (!pauseOpen && !helpOpen)
            {
                OpenPauseMenu();
            }
            else if (helpOpen == true)
            {
                CloseHelp();
            }
            else if (pauseOpen == true)
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenuButton()
    {
        if (!pauseOpen && !helpOpen)
        {
            OpenPauseMenu();
        }
        else if (helpOpen == true)
        {
            CloseHelp();
        }
        else if (pauseOpen == true)
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        pauseOpen = true;
        PauseMenuPanel.SetActive(true);
        gameManager.GetComponent<InputManager>().PauseGameForMenu();
        Debug.Log("PauseMenu wird angezeigt");
    }

    public void ClosePauseMenu()
    {
        PauseMenuPanel.SetActive(false);
        pauseOpen = false;
        gameManager.GetComponent<InputManager>().ContinueGameFromMenu();
        Debug.Log("Pause wurde geschlossen");
    }

    public void ChangeToMainMenu()
    {
        gameManager.GetComponent<InputManager>().PauseGame();
        LevelManager.Instance.LoadLevel(0);
    }

    public void showWinningScreen()
    {
        WinningScreenPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseHelp()
    {
        helpOpen=false;
        pauseOpen = true;
        HelpMenuPanel.SetActive(false);
        Debug.Log("Hilfe wurde geschlossen");

    }

    public void ShowHelp()
    {
        Debug.Log("helpback wird ausgeführt PauseMenuPanel.activeSelf:");
        Debug.Log(PauseMenuPanel.activeSelf);
        HelpMenuPanel.SetActive(true);
        helpOpen=true;

    }

    public void ChangeLevel(int sceneNumber)
    {
        gameManager.GetComponent<InputManager>().PauseGame();
        LevelManager.Instance.LoadLevel(sceneNumber);
    }

}
