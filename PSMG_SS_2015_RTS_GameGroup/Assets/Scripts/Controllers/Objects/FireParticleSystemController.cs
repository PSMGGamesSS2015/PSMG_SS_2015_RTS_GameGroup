using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FireParticleSystemController : MonoBehaviour
    {
        private List<ParticleSystem> particleSystems;
        public string SortLayerName;
        public int SortLayerPosition;

        public void Awake()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
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
        }

        public void Extinguish()
        {
            particleSystems.ForEach(ps => ps.Stop());
        }

        public void MoveToSortingLayer(string sortingLAyerName)
        {
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = sortingLAyerName);
        }
    }
}