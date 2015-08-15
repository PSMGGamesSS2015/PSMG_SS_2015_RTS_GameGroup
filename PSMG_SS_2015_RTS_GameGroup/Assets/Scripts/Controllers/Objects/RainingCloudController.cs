using Assets.Scripts.Controllers.Characters.Enemies;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class RainingCloudController : EnemyController
    {

        private ParticleSystem rain;
        public bool HasReceivedHit { get; private set; }
        public bool IsAlreadyBeingAttacked { get; set; }

        public void Awake()
        {
            rain = GetComponentInChildren<ParticleSystem>();
            HasReceivedHit = false;
            IsAlreadyBeingAttacked = false;
        }

        public void StartRaining()
        {
            rain.Play();
        }

        public void StopRaining()
        {
            rain.Stop();
        }

        public void ReceiveHit()
        {
            HasReceivedHit = true;
            StartRaining();
        }
    }
}