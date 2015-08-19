using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FireParticleSystemController : MonoBehaviour
    {
        private List<ParticleSystem> particleSystems;

        public void Awake()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
        }

        public void Start()
        {
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = SortingLayerReferences.MiddleForeground);
        }
    }
}