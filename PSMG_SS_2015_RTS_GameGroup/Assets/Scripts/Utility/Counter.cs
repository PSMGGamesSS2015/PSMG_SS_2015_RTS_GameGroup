using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{

    public class Counter : MonoBehaviour
    {
        public static Counter SetCounter(GameObject gameObject, float counterMax, Action action, bool isLoopModeActive)
        {
            var counter = gameObject.AddComponent<Counter>();
            counter.Init(counterMax, action, isLoopModeActive);
            return counter;
        }

        public static Counter SetCounter(GameObject gameObject, float counterMax, Action<GameObject> action, GameObject parameter, bool isLoopModeActive)
        {
            var counter = gameObject.AddComponent<Counter>();
            counter.Init(counterMax, action, parameter, isLoopModeActive);
            return counter;
        }

        public void Init(float counterMax, Action<GameObject> action, GameObject parameter, bool isLoopModeActive)
        {
            this.counterMax = counterMax;
            actionWithOneParameter = action;
            this.isLoopModeActive = isLoopModeActive;
            this.parameter = parameter;
            isInitialized = true;
        }

        public float CurrentCount { get; private set; }
        private float counterMax;

        private Action action;
        private Action<GameObject> actionWithOneParameter;
        private GameObject parameter;

        private bool isLoopModeActive;
        private bool isPaused;
        private bool isInitialized;

        public void Init(float counterMax, Action action, bool isLoopModeActive)
        {
            this.counterMax = counterMax;
            this.action = action;
            this.isLoopModeActive = isLoopModeActive;

            isInitialized = true;
        }

        public void Update()
        {
            if (!isInitialized) return;
            if (CurrentCount >= counterMax)
            {
                if (action != null)
                {
                    action();
                }
                else
                {
                    actionWithOneParameter(parameter);
                }
                
                if (!isLoopModeActive)
                {
                    Discard();
                }
                else
                {
                    ResetCounter();
                }
            }

            if (!isPaused)
            {
                CurrentCount += Time.deltaTime;
            }
        }

        private void ResetCounter()
        {
            CurrentCount = 0f;
        }

        private void Discard()
        {
            Destroy(this);
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        public void Stop()
        {
            isPaused = true;
            isLoopModeActive = false;
            action = null;
            Discard();
        }

    }
}