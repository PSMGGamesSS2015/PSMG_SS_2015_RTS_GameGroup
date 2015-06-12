using System.Collections.Generic;
using Assets.Scripts.Config;
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

        void LevelManager.ILevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
        {
            var userInterface = Instantiate(UserInterfacePrefab).GetComponent<UserInterface.UserInterface>();
            userInterface.Setup(config);
            foreach (var listener in listeners)
            {
                listener.OnUserInterfaceLoaded(userInterface);
            }
        }
    }
}