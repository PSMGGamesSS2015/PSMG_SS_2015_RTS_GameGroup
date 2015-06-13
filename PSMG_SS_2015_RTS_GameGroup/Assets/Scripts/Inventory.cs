using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Inventory : MonoBehaviour
    {
        private Dictionary<string, SpriteRenderer> items;
        protected string[] TagNames;

        public void Awake()
        {
            items = new Dictionary<string, SpriteRenderer>();
            InitTagNames();
        }

        protected abstract void InitTagNames();

        public void Start()
        {
            RetrieveItems();
            HideItems();
        }

        public virtual void HideItems()
        {
            foreach (var item in items)
            {
                item.Value.enabled = false;
            }
        }

        protected virtual void RetrieveItems()
        {
            var renderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (var renderer in renderers)
            {
                var renderer1 = renderer;
                foreach (var tagName in TagNames.Where(tagName => renderer1.gameObject.tag == tagName))
                {
                    items.Add(tagName, renderer);
                }
            }
        }

        public void Display(string tagName)
        {
            HideItems();
            items[tagName].enabled = true;
        }

    }
}