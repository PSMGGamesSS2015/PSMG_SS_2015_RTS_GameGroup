using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FieryCakeController : MonoBehaviour
    {
        private List<FireController> flames;
        private bool hasBeenExtinguished;

        public void Awake()
        {
            hasBeenExtinguished = false;
            flames = GetComponentsInChildren<FireController>().ToList();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != TagReferences.Imp) return;

            if (!hasBeenExtinguished)
            {
                LightFires();
            }
        }

        private void LightFires()
        {
            flames.ForEach(f => f.gameObject.SetActive(true));
        }

        public void Extinguish()
        {
            flames.ForEach(f => f.gameObject.SetActive(false));
        }
    }
}