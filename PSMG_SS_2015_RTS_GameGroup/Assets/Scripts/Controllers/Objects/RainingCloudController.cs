using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class RainingCloudController : MonoBehaviour
    {

        private ParticleSystem rain;

        public void Awake()
        {
            rain = GetComponentInChildren<ParticleSystem>();
        }

        public void StartRaining()
        {
            rain.Play();
        }

        public void StopRaining()
        {
            rain.Stop();
        }

    }
}