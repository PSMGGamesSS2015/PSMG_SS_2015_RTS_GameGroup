using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Config;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.UserInterfaceComponents
{
    public class UserInterface : MonoBehaviour, ImpManager.IMpManagerListener
    {
        private int[] currentMaxProfessions;

        public ImpTrainingButton[] ImpTrainingButtons { get; private set; }
        public Canvas UICanvas { get; private set; }

        public void Awake()
        {
            RetrieveComponents();
            currentMaxProfessions = LevelManager.Instance.CurrentLevelConfig.MaxProfessions;
        }

        private void RetrieveComponents()
        {
            ImpTrainingButtons = GetComponentsInChildren<ImpTrainingButton>();

            UICanvas = GetComponentsInChildren<Canvas>().ToList().Find(c => c.tag == TagReferences.UICanvas);
        }

        public void Setup(LevelConfig config)
        {
            currentMaxProfessions = config.MaxProfessions;
            
            for (var i = 0; i < ImpTrainingButtons.Length; i++)
            {
                ImpTrainingButtons[i].Counter.text = "0/" + currentMaxProfessions[i];
                
            }
        }

        void ImpManager.IMpManagerListener.OnUpdateMaxProfessions(int[] professions)
        {
            for (var i = 0; i < ImpTrainingButtons.Length; i++)
            {
                ImpTrainingButtons[i].Counter.text = professions[i] + "/" + currentMaxProfessions[i];
            }
        }
    }
}