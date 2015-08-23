using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FlourBagController : MonoBehaviour
    {
        public GameObject FlourParticleSystemPrefab;
        private bool hasExploded;

        private ParticleSystem flourParticleSystem;

        public void Awake()
        {
            hasExploded = false;
        }

        public void Explode()
        {
            if (hasExploded) return;

            hasExploded = true;
            var flourParticleSystemGameObject = (GameObject)Instantiate(FlourParticleSystemPrefab, gameObject.transform.position, Quaternion.identity);
            flourParticleSystem = flourParticleSystemGameObject.GetComponent<ParticleSystem>();
            flourParticleSystem.Play();

            Counter.SetCounter(gameObject, 3f, StopPlayingParticleSystem, false);
        }

        private void StopPlayingParticleSystem()
        {
            flourParticleSystem.Stop();

            Destroy(gameObject);
        }
    }
}