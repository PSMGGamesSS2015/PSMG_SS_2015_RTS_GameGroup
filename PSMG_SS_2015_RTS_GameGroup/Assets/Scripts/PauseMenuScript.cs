using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PauseMenuScript : MonoBehaviour {

        private GameObject pauseMenuPanel, helpMenuPanel, winningScreenPanel;
        private bool pauseOpen;
        private bool helpOpen;

        private const string ContinueGameButtonName = "ContinueGameButton";
        private const string RestartGameButtonName = "RestartGameButton";
        private const string HelpButtonName = "HelpButton";
        private const string MainMenuButtonName = "MainMenuButton";
        private const string GameEndButtonName = "GameEndButton";
        private const string HelpPanelBackButtonName = "HelpPanelBackButton";

        private Button continueGameButton;
        private Button restartGameButton;
        private Button helpButton;
        private Button mainMenuButton;
        private Button gameEndButton;
        private Button helpPanelBackButton;

        public void Awake()
        {
            pauseMenuPanel = GameObject.FindWithTag(TagReferences.PausePanel);
            helpMenuPanel = GameObject.FindWithTag(TagReferences.HelpPanel);
            winningScreenPanel = GameObject.FindWithTag(TagReferences.WinningScreen);
        }

        public void Start()
        {
            RetrieveButtons();
            RegisterButtonFunctions();
            
            winningScreenPanel.SetActive(false);
            helpMenuPanel.SetActive(false);
            pauseMenuPanel.SetActive(false);

            pauseOpen = false;
            helpOpen = false;
        }

        private void RegisterButtonFunctions()
        {
            continueGameButton.onClick.AddListener(OnContinueGameButtonClick);
            restartGameButton.onClick.AddListener(OnRestartGameButtonClick);
            helpButton.onClick.AddListener(OnHelpButtonClick);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
            gameEndButton.onClick.AddListener(OnGameEndButtonClick);
            helpPanelBackButton.onClick.AddListener(OnHelpPanelBackButtonClick);
        }

        private void OnHelpPanelBackButtonClick()
        {
            CloseHelp();
        }

        private void OnGameEndButtonClick()
        {
            QuitGame();
        }

        private void OnMainMenuButtonClick()
        {
            Time.timeScale = 1.0f;
            InputManager.Instance.CurrentSpeed = InputManager.GameSpeed.Normal;
            ChangeToMainMenu();
        }

        private void OnHelpButtonClick()
        {
            ShowHelp();
        }

        private void OnRestartGameButtonClick()
        {
            Time.timeScale = 1.0f;
            InputManager.Instance.CurrentSpeed = InputManager.GameSpeed.Normal;
            LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevelNumber);
        }

        private void OnContinueGameButtonClick()
        {
            ClosePauseMenu();
        }

        private void RetrieveButtons()
        {
            var buttons = GetComponentsInChildren<Button>().ToList();

            continueGameButton = buttons.First(b => b.name == ContinueGameButtonName);
            restartGameButton = buttons.First(b => b.name == RestartGameButtonName);
            helpButton = buttons.First(b => b.name == HelpButtonName);
            mainMenuButton = buttons.First(b => b.name == MainMenuButtonName);
            gameEndButton = buttons.First(b => b.name == GameEndButtonName);
            helpPanelBackButton = buttons.First(b => b.name == HelpPanelBackButtonName);
        }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            OpenPauseMenuButton();
        }

        public void OpenPauseMenuButton()
        {
            if (!pauseOpen && !helpOpen)
            {
                OpenPauseMenu();
            }
            else if (helpOpen)
            {
                CloseHelp();
            }
            else if (pauseOpen)
            {
                ClosePauseMenu();
            }
        }

        public void OpenPauseMenu()
        {
            pauseOpen = true;
            pauseMenuPanel.SetActive(true);
            GameManager.Instance.GetComponent<InputManager>().PauseGameForMenu();
        }

        private void ClosePauseMenu()
        {
            pauseMenuPanel.SetActive(false);
            pauseOpen = false;
            GameManager.Instance.GetComponent<InputManager>().ContinueGameFromMenu();
        }

        public void ChangeToMainMenu()
        {
            LevelManager.Instance.LoadLevel(0);
        }

        public void ShowWinningScreen()
        {
            winningScreenPanel.SetActive(true);
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        public void CloseHelp()
        {
            helpOpen  =false;
            pauseOpen = true;
            helpMenuPanel.SetActive(false);
        }

        private void ShowHelp()
        {
            helpMenuPanel.SetActive(true);
            helpOpen=true;
        }

        public void ChangeLevel(int sceneNumber)
        {
            GameManager.Instance.GetComponent<InputManager>().PauseGame();
            LevelManager.Instance.LoadLevel(sceneNumber);
        }

    }
}
