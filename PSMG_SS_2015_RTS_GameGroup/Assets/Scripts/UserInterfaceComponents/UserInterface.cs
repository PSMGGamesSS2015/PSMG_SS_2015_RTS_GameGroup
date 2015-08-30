using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Config;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterfaceComponents
{
    public class UserInterface : MonoBehaviour, ImpManager.IMpManagerListener
    {
        private int[] currentMaxProfessions;

        public ImpTrainingButton[] ImpTrainingButtons { get; private set; }
        public Canvas UICanvas { get; private set; }
        private Button menuButton;
        public PauseMenuScript PauseMenuScript { get; private set; }

        private const string MenuButtonName = "MenuButton";

        public void Awake()
        {
            RetrieveComponents();
            currentMaxProfessions = LevelManager.Instance.CurrentLevel.CopyOfMaxProfessions;
        }

        private void RetrieveComponents()
        {
            PauseMenuScript = GetComponentInChildren<PauseMenuScript>();

            ImpTrainingButtons = GetComponentsInChildren<ImpTrainingButton>();
            var buttons = GetComponentsInChildren<Button>().ToList();
            menuButton = buttons.First(b => b.name == MenuButtonName);
            menuButton.onClick.AddListener(OnMenuButtonClick);

            UICanvas = GetComponentsInChildren<Canvas>().First(c => c.tag == TagReferences.UICanvas);
        }

        private void OnMenuButtonClick()
        {
            PauseMenuScript.OpenPauseMenuButton();
        }

        public void Setup(LevelConfig config)
        {
            currentMaxProfessions = LevelManager.Instance.CurrentLevel.CopyOfMaxProfessions;
            
            for (var i = 0; i < ImpTrainingButtons.Length-1; i++)
            {
                ImpTrainingButtons[i].Counter.text = "0/" + currentMaxProfessions[i];
                
            }
        }

        void ImpManager.IMpManagerListener.OnUpdateMaxProfessions(int[] professions)
        {
            for (var i = 0; i < ImpTrainingButtons.Length-1; i++)
            {
                ImpTrainingButtons[i].Counter.text = professions[i] + "/" + currentMaxProfessions[i];
            }
        }
    }
}