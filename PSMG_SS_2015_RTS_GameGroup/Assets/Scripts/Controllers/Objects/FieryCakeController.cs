using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FieryCakeController : MonoBehaviour
    {
        private bool hasBeenExtinguished;
        private List<GameObject> fire; 

        public void Awake()
        {
            hasBeenExtinguished = false;
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
            var spawnPosition = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
            fire = SpecialEffectsManager.Instance.SpawnFire(spawnPosition, SortingLayerReferences.MiddleForeground);

            hasBeenExtinguished = true;
        }

        public void Extinguish()
        {
            fire.ForEach(f => f.GetComponent<FireParticleSystemController>().Extinguish());
        }
    }
}