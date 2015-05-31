using UnityEngine;
using System.Collections;
using System;

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
    private float currentCount;
    private float counterMax;
  
    private Func<Void> Action;

    private bool isLoopModeActive;
    private bool isPaused;
    private bool isInitialized = false;

    public void Init(float counterMax, Func<Void> Action, bool isLoopModeActive)
    {
        this.counterMax = counterMax;
        this.Action = Action;
        this.isLoopModeActive = isLoopModeActive;

        isInitialized = true;
    }

    private void Update()
    {
        if (isInitialized)
        {
            if (currentCount >= counterMax)
            {
                Debug.Log("Performing Action");
                Action();
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
    }

    private void ResetCounter()
    {
        currentCount = 0f;
    }

    private void Discard()
    {
        Destroy(gameObject);
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
        Discard();
    }

}