using Assets.Scripts.Config;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.UserInterface
{
    public class UserInterface : MonoBehaviour, ImpManager.IMpManagerListener
    {
        private ImpTrainingButton[] impTrainingButtons;
        private int[] currentMaxProfessions;

        public ImpTrainingButton[] ImpTrainingButtons
        {
            get
            {
                return impTrainingButtons;
            }
        }

        public void Awake()
        {
            RetrieveComponents();
        }

        private void RetrieveComponents()
        {
            impTrainingButtons = GetComponentsInChildren<ImpTrainingButton>();
        }

        public void Setup(LevelConfig config)
        {
            currentMaxProfessions = config.MaxProfessions;
        }

        public void Start()
        {
            if (currentMaxProfessions == null) return;
            for (var i = 0; i < impTrainingButtons.Length; i++)
            {
                impTrainingButtons[i].Counter.text = currentMaxProfessions[i].ToString();
            }
        }

        void ImpManager.IMpManagerListener.OnUpdateMaxProfessions(int[] professions)
        {
            for (var i = 0; i < impTrainingButtons.Length; i++)
            {
                impTrainingButtons[i].Counter.text = (currentMaxProfessions[i] - professions[i]).ToString();
            }
        }
    }
}