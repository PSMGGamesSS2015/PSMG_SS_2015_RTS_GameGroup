using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class RainingCloudController : EnemyController
    {

        private ParticleSystem rain;
        public bool HasReceivedHit { get; private set; }
        public bool IsAlreadyBeingAttacked { get; set; }

        private FieryCakeController fieryCakeController;

        public void Awake()
        {
            rain = GetComponentInChildren<ParticleSystem>();
            HasReceivedHit = false;
            IsAlreadyBeingAttacked = false;

            fieryCakeController = GameObject.FindGameObjectWithTag(TagReferences.FieryCake).GetComponent<FieryCakeController>();
        }

        public void StartRaining()
        {
            rain.Play();
            Counter.SetCounter(gameObject, 1.0f, fieryCakeController.Extinguish, false);
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