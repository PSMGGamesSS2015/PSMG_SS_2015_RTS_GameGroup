using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FieryCakeController : MonoBehaviour
    {

        public bool HasBeenLit { get; private set; }
        public bool HasBeenExtinguished { get; private set; }

        private List<GameObject> fire; 

        public void Awake()
        {
            HasBeenExtinguished = false;
            HasBeenLit = false;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != TagReferences.Imp) return;

            if (HasBeenExtinguished || HasBeenLit) return;

            LightFires();
            HasBeenLit = true;
        }


        private void LightFires()
        {
            fire = SpecialEffectsManager.Instance.SpawnFire(gameObject.transform.position, SortingLayerReferences.MiddleForeground);
        }

        public void Extinguish()
        {
            HasBeenExtinguished = true;
            fire.ForEach(f => f.GetComponent<FireParticleSystemController>().Extinguish());
        }
    }
}