using System.Collections.Generic;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class CollectableObject : MonoBehaviour
    {

        public interface ICollectableObjectListener
        {
            void OnCollected(CollectableObject self);
        }

        private List<ICollectableObjectListener> listeners;

        public void Awake()
        {
            listeners = new List<ICollectableObjectListener>();
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.isTrigger) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;
            Collect();
        }

        public void RegisterListener(ICollectableObjectListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ICollectableObjectListener listener)
        {
            listeners.Remove(listener);
        }

        private void Collect()
        {
            if (listeners.Count == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                listeners.ForEach(l => l.OnCollected(this));
            }
        }
    }
}
