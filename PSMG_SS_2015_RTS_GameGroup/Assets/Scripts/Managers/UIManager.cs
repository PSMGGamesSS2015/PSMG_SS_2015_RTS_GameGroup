using System.Collections.Generic;
using Assets.Scripts.Managers.UIManagerAndServices;
using Assets.Scripts.ParameterObjects;
using Assets.Scripts.UserInterfaceComponents;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour, LevelManager.ILevelManagerListener
    {
        public GameObject UserInterfacePrefab;

        private List<IUIManagerListener> listeners;

        public static UIManager Instance;

        public UserInterface CurrentUserInterface { get; set; }

        public UIMessageService UIMessageService { get; private set; }

        public UIImpOutOfSightService UIImpOutOfSightService { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            listeners = new List<IUIManagerListener>();
            InitComponents();
        }

        private void InitComponents()
        {
            UIMessageService = GetComponent<UIMessageService>();
            UIImpOutOfSightService = GetComponent<UIImpOutOfSightService>();
        }

        public interface IUIManagerListener
        {
            void OnUserInterfaceLoaded(UserInterface userInteface);
        }

        public void RegisterListener(IUIManagerListener listener)
        {
            listeners.Add(listener);
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            CurrentUserInterface = Instantiate(UserInterfacePrefab).GetComponent<UserInterface>();
            CurrentUserInterface.Setup(level.CurrentLevelConfig);

            GetComponent<ImpManager>().OnNewUserInterfaceLoaded(CurrentUserInterface);
            listeners.ForEach(x => x.OnUserInterfaceLoaded(CurrentUserInterface));
        }

        void LevelManager.ILevelManagerListener.OnStartMessagePlayed()
        {
            // TODO
        }
    }
}