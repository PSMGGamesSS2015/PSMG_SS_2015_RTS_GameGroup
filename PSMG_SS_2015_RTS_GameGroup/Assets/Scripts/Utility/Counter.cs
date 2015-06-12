using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    /// <summary>
    /// A counter executes an action passed to it after a specified amount of time.
    /// It can be configured to do so repeatedly. 
    /// Use:
    /// 1) Attach this script to an empty gameobject and create a prefab.
    /// 2) Via the inspector, add the gameobject to a script that needs a counter .
    /// 3) Instantiate the counter when needed.
    /// 4) Configure the counter by using the 'Init' method
    /// </summary>

    public class Counter : MonoBehaviour
    {
        public static Counter SetCounter(GameObject gameObject, float counterMax, Action action, bool isLoopModeActive)
        {
            var counter = gameObject.AddComponent<Counter>();
            counter.Init(counterMax, action, isLoopModeActive);
            return counter;
        }

        private float currentCount;
        private float counterMax;

        private Action action;

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
            if (currentCount >= counterMax)
            {
                action();
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
                currentCount += Time.deltaTime;
            }
        }

        private void ResetCounter()
        {
            currentCount = 0f;
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