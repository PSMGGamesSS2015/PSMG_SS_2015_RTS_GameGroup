using System.Collections.Generic;
using Assets.Scripts.Managers.UIManagerAndServices;
using Assets.Scripts.ParameterObjects;
using Assets.Scripts.UserInterfaceComponents;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour, LevelManager.ILevelManagerListener, LevelManager.ILevelManagerMenuSceneListener, LevelManager.ILevelManagerNarrativeSceneListener
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
            UIMessageService.Reset();
            UIImpOutOfSightService.Reset();
            
            CurrentUserInterface = Instantiate(UserInterfacePrefab).GetComponent<UserInterface>();
            CurrentUserInterface.Setup(level.CurrentLevelConfig);

            ImpManager.Instance.RegisterListener(CurrentUserInterface);
            listeners.ForEach(x => x.OnUserInterfaceLoaded(CurrentUserInterface));
        }

        void LevelManager.ILevelManagerListener.OnStartMessagePlayed()
        {
            // TODO
        }

        void LevelManager.ILevelManagerListener.OnLevelEnding()
        {
            // TODO
        }

        void LevelManager.ILevelManagerMenuSceneListener.OnMenuLevelStarted(Level level)
        {
            UIMessageService.Reset();
            UIImpOutOfSightService.Reset();
        }

        void LevelManager.ILevelManagerNarrativeSceneListener.OnNarrativeLevelStarted(Level level)
        {
            UIMessageService.Reset();
            UIImpOutOfSightService.Reset();
        }
    }
}