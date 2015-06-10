using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, LevelManager.LevelManagerListener
{
    public GameObject userInterfacePrefab;

    private List<UIManagerListener> listeners;

    private void Awake()
    {
        listeners = new List<UIManagerListener>();
    }

    public interface UIManagerListener
    {
        void OnUserInterfaceLoaded(UserInterface userInteface);       
    }

    public void RegisterListener(UIManagerListener listener)
    {
        listeners.Add(listener);
    }

    void LevelManager.LevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
    {
        UserInterface userInterface = Instantiate(userInterfacePrefab).GetComponent<UserInterface>();
        userInterface.Setup(config);
        foreach (UIManagerListener listener in listeners)
        {
            listener.OnUserInterfaceLoaded(userInterface);
        }
    }
}