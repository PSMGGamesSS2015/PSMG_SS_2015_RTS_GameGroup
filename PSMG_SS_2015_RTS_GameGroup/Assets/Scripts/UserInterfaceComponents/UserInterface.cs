using Assets.Scripts.Config;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.UserInterfaceComponents
{
    public class UserInterface : MonoBehaviour, ImpManager.IMpManagerListener
    {
        private int[] currentMaxProfessions;

        public ImpTrainingButton[] ImpTrainingButtons { get; private set; }

        public void Awake()
        {
            RetrieveComponents();
        }

        private void RetrieveComponents()
        {
            ImpTrainingButtons = GetComponentsInChildren<ImpTrainingButton>();
        }

        public void Setup(LevelConfig config)
        {
            currentMaxProfessions = config.MaxProfessions;
        }

        public void Start()
        {
            if (currentMaxProfessions == null) return;
            for (var i = 0; i < ImpTrainingButtons.Length; i++)
            {
                ImpTrainingButtons[i].Counter.text = currentMaxProfessions[i].ToString();
            }
        }

        void ImpManager.IMpManagerListener.OnUpdateMaxProfessions(int[] professions)
        {
            for (var i = 0; i < ImpTrainingButtons.Length; i++)
            {
                ImpTrainingButtons[i].Counter.text = (currentMaxProfessions[i] - professions[i]).ToString();
            }
        }

    }
}