using Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices;
using Assets.Scripts.LevelScripts;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FurnaceController : MonoBehaviour
    {
        public bool IsLight { get; set; }
        private FireParticleSystemController fireParticleSystem;

        public void Awake()
        {
            fireParticleSystem = GetComponentInChildren<FireParticleSystemController>();
            IsLight = false;
        }

        public void Start()
        {
            fireParticleSystem.Extinguish();
        }

        public void Light()
        {
            fireParticleSystem.Light();
            IsLight = true;

            var level06Events = (Level06Events) LevelManager.Instance.CurrentLevelEvents;
            if (level06Events != null)
            {
                var knight = level06Events.KnightAtEndOfFirstFloor;
                if (knight != null)
                {
                    knight.GetComponent<KnightFeelsSoHotService>().Heat();
                }
            }
        }

        public void Extinguish()
        {
            if (!IsLight) return;

            fireParticleSystem.Extinguish();
            IsLight = false;
        }
    }
}