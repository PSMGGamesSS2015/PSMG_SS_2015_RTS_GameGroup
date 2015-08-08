using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class GaslightController : MonoBehaviour
    {
        public bool IsLight;

        private Light lightSource;
        
        public void Awake()
        {
            lightSource = GetComponentInChildren<Light>();

            if (IsLight) Light();
        }

        public void Light()
        {
            IsLight = true;
            lightSource.enabled = true;
        }

        public void GoOut()
        {
            IsLight = false;
            lightSource.enabled = false;
        }
    }
}