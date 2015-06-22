using System.Collections.Generic;
using Assets.Scripts.ParameterObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour, LevelManager.ILevelManagerListener
    {
        public GameObject UserInterfacePrefab;

        private List<IUIManagerListener> listeners;

        public void Awake()
        {
            listeners = new List<IUIManagerListener>();
        }

        public interface IUIManagerListener
        {
            void OnUserInterfaceLoaded(UserInterface.UserInterface userInteface);       
        }

        public void RegisterListener(IUIManagerListener listener)
        {
            listeners.Add(listener);
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            var userInterface = Instantiate(UserInterfacePrefab).GetComponent<UserInterface.UserInterface>();
            userInterface.Setup(level.CurrentLevelConfig);
            listeners.ForEach(x => x.OnUserInterfaceLoaded(userInterface));
        }
    }
}