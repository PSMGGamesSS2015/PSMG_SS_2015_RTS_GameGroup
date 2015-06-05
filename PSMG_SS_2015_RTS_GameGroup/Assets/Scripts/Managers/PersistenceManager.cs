using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistenceManager : MonoBehaviour
{

    private List<PersistenceManagerListener> listeners;

    public interface PersistenceManagerListener
    {
        // TODO
    }

    private void Awake()
    {
        listeners = new List<PersistenceManagerListener>();
    }

    public void RegisterListener(PersistenceManagerListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(PersistenceManagerListener listener)
    {
        listeners.Remove(listener);
    }

    public void LoadGame()
    {
        // TODO
    }

}