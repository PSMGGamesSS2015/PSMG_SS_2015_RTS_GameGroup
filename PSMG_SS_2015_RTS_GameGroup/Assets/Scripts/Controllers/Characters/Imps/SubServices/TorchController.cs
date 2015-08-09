using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class TorchController : MonoBehaviour
    {
        private SpriteRenderer[] components;
        private Light torchLight;

        public void Awake()
        {
            components = GetComponentsInChildren<SpriteRenderer>();
            torchLight = GetComponent<Light>();
        }


        public void Display()
        {
            components.ToList().ForEach(c => c.enabled = true);
            torchLight.enabled = true;
        }

        public void Hide()
        {
            components.ToList().ForEach(c => c.enabled = false);
            torchLight.enabled = false;
        }
    }
}