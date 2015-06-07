using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour {

    private List<GoalControllerListener> listeners;

    public interface GoalControllerListener
    {
        void OnGoalReachedByImp();
    }

    private void Awake()
    {
        listeners = new List<GoalControllerListener>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == TagReferences.IMP)
        {
            foreach (GoalControllerListener listener in listeners)
            {
                listener.OnGoalReachedByImp();
            }
        }

    }

    public void RegisterListener(GoalControllerListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterLIstener(GoalControllerListener listener)
    {
        listeners.Remove(listener);
    }

}
