using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FireParticleSystemController : MonoBehaviour
    {
        private List<ParticleSystem> particleSystems;
        private new Light light; 

        public string SortLayerName;
        public int SortLayerPosition;

        public void Awake()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
            light = GetComponentInChildren<Light>();
        }

        public void Start()
        {
            if (SortLayerName == "") SortLayerName = SortingLayerReferences.Default;
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = SortLayerName);

            if (SortLayerPosition == -1) return;
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingOrder = SortLayerPosition);
        }

        public void Light()
        {
            particleSystems.ForEach(ps => ps.Play());
            light.enabled = true;
        }

        public void Extinguish()
        {
            particleSystems.ForEach(ps => ps.Stop());
            light.enabled = false;
        }

        public void MoveToSortingLayer(string sortingLAyerName)
        {
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = sortingLAyerName);
        }
    }
}