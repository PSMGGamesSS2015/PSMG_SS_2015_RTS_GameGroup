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
        }

        public void Start()
        {
            flames = GetComponentsInChildren<FireController>().ToList();
            flames.ForEach(f => f.Hide());
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
            flames.ForEach(f => f.Display());
        }

        public void Extinguish()
        {
            flames.ForEach(f => f.Hide());
        }
    }
}