﻿using System.Collections.Generic;
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

        public UserInterface CurrentUserInterface;

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
            listeners.ForEach(x => x.OnUserInterfaceLoaded(CurrentUserInterface));
        }
    }
}