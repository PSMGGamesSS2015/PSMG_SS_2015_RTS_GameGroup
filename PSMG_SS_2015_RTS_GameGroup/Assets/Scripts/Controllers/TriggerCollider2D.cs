using UnityEngine;

// TODO improve listener interface

namespace Assets.Scripts.Controllers
{
    /// <summary>
    /// The TriggerCollider2D script can be attached to a GameObject with a Collider2D that is set as trigger.
    /// It then provides information about collisions to parent GameObjects. 
    /// Parent GameObjects have to implement the TriggerCollider2DListener interface and register as listeners.
    /// </summary>

    public class TriggerCollider2D : MonoBehaviour {

        public interface ITriggerCollider2DListener
        {
            void OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider);
            void OnTriggerExit2D(TriggerCollider2D self, Collider2D collider);
            void OnTriggerStay2D(TriggerCollider2D self, Collider2D collider);
        }

        private ITriggerCollider2DListener listener;

        public void RegisterListener(ITriggerCollider2DListener listener)
        {
            this.listener = listener;
        }

        public void UnregisterListener()
        {
            listener = null;
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (listener != null)
            listener.OnTriggerExit2D(this, collider);
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (listener != null)
            listener.OnTriggerEnter2D(this, collider);
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (listener != null)
            listener.OnTriggerStay2D(this, collider);
        }

    }
}
