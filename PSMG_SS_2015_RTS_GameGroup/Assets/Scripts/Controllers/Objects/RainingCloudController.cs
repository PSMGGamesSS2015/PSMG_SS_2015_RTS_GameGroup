using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class RainingCloudController : MonoBehaviour
    {

        private GameObject rain;

        public void Awake()
        {
            rain = GetComponent<ParticleSystem>().gameObject;
        }

        public void StartRaining()
        {
            rain.SetActive(true);
        }

        public void StopRaining()
        {
            rain.SetActive(false);
        }

    }
}