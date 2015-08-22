using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class DragonFireController : MonoBehaviour
    {
        private FireParticleSystemController fireParticleSystem;

        public void Awake()
        {
            fireParticleSystem = GetComponent<FireParticleSystemController>();

            Counter.SetCounter(gameObject, 2.0f, Extinguish, false);
        }

        private void Extinguish()
        {
            fireParticleSystem.Extinguish();

            Counter.SetCounter(gameObject, 1.0f, DestroyDragonFire, false);
        }

        private void DestroyDragonFire()
        {
            Destroy(gameObject);
        }
    }
}