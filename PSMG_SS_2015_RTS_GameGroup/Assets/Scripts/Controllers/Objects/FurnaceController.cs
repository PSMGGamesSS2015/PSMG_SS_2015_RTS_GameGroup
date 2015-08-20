using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FurnaceController : MonoBehaviour
    {
        public bool IsLight { get; private set; }
        private FireParticleSystemController fireParticleSystem;

        public void Awake()
        {
            fireParticleSystem = GetComponent<FireParticleSystemController>();
            IsLight = false;
        }

        public void Start()
        {
            fireParticleSystem.Extinguish();
        }

        public void Light()
        {
            if (IsLight) return; 

            fireParticleSystem.Light();
            IsLight = true;
        }

        public void Extinguish()
        {
            if (!IsLight) return;

            fireParticleSystem.Extinguish();
            IsLight = false;
        }
    }
}