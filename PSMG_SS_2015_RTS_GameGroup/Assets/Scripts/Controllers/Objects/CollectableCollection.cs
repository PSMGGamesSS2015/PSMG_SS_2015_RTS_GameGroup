using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    class CollectableCollection : MonoBehaviour, CollectableObject.ICollectableObjectListener
    {
        private List<CollectableObject> collectionItems;

        public void Awake()
        {
            collectionItems = GetComponentsInChildren<CollectableObject>().ToList();
        }

        public void Start()
        {
            collectionItems.ForEach(ci => ci.RegisterListener(this));
        }

        public void CollectItems()
        {
            collectionItems.ForEach(ci => ci.GetComponent<SpriteRenderer>().enabled = false);
            collectionItems.ForEach(ci => ci.UnregisterListener(this));
            collectionItems.ForEach(Destroy);
        }

        void CollectableObject.ICollectableObjectListener.OnCollected(CollectableObject self)
        {
            CollectItems();
        }
    }
}