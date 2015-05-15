using UnityEngine;
using System.Collections;

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

    private void OnTriggerExit2D(Collider2D collider)
    {
        listener.OnTriggerExit2D(this, collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        listener.OnTriggerEnter2D(this, collider);
    }

}
