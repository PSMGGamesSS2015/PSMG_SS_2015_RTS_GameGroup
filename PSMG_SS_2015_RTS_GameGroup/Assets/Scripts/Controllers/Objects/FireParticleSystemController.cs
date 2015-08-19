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

        public void Awake()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        }

        public void Start()
        {
            if (SortLayerName == "") SortLayerName = SortingLayerReferences.Default;
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = SortLayerName);
        }

        public void MoveToSortingLayer(string sortingLAyerName)
        {
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = sortingLAyerName);
        }
    }
}