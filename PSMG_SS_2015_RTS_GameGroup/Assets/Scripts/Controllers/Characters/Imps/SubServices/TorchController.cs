using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class TorchController : MonoBehaviour
    {
        private SpriteRenderer[] components;
        private Light torchLight;
        private List<ParticleSystem> torchParticleSystems; 

        public void Awake()
        {
            components = GetComponentsInChildren<SpriteRenderer>();
            torchLight = GetComponentInChildren<Light>();

            torchParticleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        }


        public void Display()
        {
            components.ToList().ForEach(c => c.enabled = true);
            torchLight.enabled = true;
            torchParticleSystems.ForEach(tps => tps.Play());
        }

        public void Hide()
        {
            components.ToList().ForEach(c => c.enabled = false);
            torchLight.enabled = false;
            torchParticleSystems.ForEach(tps => tps.Stop());
        }
    }
}