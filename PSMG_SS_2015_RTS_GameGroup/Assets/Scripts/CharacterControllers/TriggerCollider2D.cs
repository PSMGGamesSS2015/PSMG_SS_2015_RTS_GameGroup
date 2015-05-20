using UnityEngine;
using System.Collections;

/// <summary>
/// The TriggerCollider2D script can be attached to a GameObject with a Collider2D that is set as trigger.
/// It then provides information about collisions to parent GameObjects. 
/// Parent GameObjects have to implement the TriggerCollider2DListener interface and register as listeners.
/// </summary>

public class TriggerCollider2D : MonoBehaviour {

    public interface TriggerCollider2DListener
    {
        void OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider);
        void OnTriggerExit2D(TriggerCollider2D self, Collider2D collider);    
    }

    private TriggerCollider2DListener listener;

    public void RegisterListener(TriggerCollider2DListener listener)
    {
        this.listener = listener;
    }

    public void UnregisterListener()
    {
        listener = null;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        listener.OnTriggerExit2D(this, collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        listener.OnTriggerEnter2D(this, collider);
    }

}
