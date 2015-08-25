using System.Collections.Generic;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Controllers.Objects.Water;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SpecialEffectsManager : MonoBehaviour
    {
        public GameObject FirePrefab;

        public static SpecialEffectsManager Instance;

        public const float StandardScale = 0.1f;
        public const float StandardRotation = 0.1f;

        private const float VariationLimiter = 0.35f;
        public Water Water { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            Water = gameObject.AddComponent<Water>();
            gameObject.AddComponent<WaterDetector>();
        }

        // position in world coordinates
        public List<GameObject> SpawnFire(Vector3 position, string sortingLayerName)
        {
            return SpawnFire(position, sortingLayerName, new Vector3(StandardScale, StandardScale, StandardScale), Quaternion.identity, 1);   
        }

        public List<GameObject> SpawnFire(Vector3 position, string sortingLayerName, int positionInLayer, int nrOfFlameTongues)
        {
            return SpawnFire(position, sortingLayerName, new Vector3(StandardScale, StandardScale, StandardScale), Quaternion.identity, nrOfFlameTongues);   
        }

        public List<GameObject> SpawnFire(Vector3 position, string sortingLayerName, Vector3 scale, Quaternion rotation, int nrOfFlameTongues)
        {
            var flames = new List<GameObject>();

            var flame = (GameObject) Instantiate(FirePrefab, position, rotation);
            flame.GetComponent<FireParticleSystemController>().MoveToSortingLayer(sortingLayerName);

            flames.Add(flame);

            if (nrOfFlameTongues > 1)
            {
                for (var i = 0; i < nrOfFlameTongues - 1; i++)
                {
                    var xVaration = Random.value - VariationLimiter;

                    var yVaration = Random.value - VariationLimiter;

                    var additionalFlame = (GameObject) Instantiate(FirePrefab, new Vector3(position.x + xVaration, position.y + yVaration, position.z), rotation);
                    additionalFlame.GetComponent<FireParticleSystemController>().MoveToSortingLayer(sortingLayerName);

                    flames.Add(additionalFlame);
                }
            }
            return flames;
        }


        public List<GameObject> SpawnFire(Vector3 position, string sortingLayerName, int positionInLayer)
        {
            var flames = new List<GameObject>();

            var flame = (GameObject)Instantiate(FirePrefab, position, Quaternion.identity);
            flame.GetComponent<FireParticleSystemController>().MoveToSortingLayer(sortingLayerName);
            flame.GetComponent<FireParticleSystemController>().MoveToSortingLayerPosition(positionInLayer);
            flames.Add(flame);

            return flames;
        }
    }
}