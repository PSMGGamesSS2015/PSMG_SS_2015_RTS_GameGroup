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
        }

        // position in world coordinates
        public void SpawnFire(Vector3 position)
        {
            SpawnFire(new Vector3(position.x + 1f, position.y + 0.5f, position.z), new Vector3(StandardScale, StandardScale, StandardScale), Quaternion.identity, 1);   
        }

        public void SpawnFire(Vector3 position, int nrOfFlameTongues)
        {
            SpawnFire(new Vector3(position.x + 1f, position.y + 0.5f, position.z), new Vector3(StandardScale, StandardScale, StandardScale), Quaternion.identity, nrOfFlameTongues);   
        }

        public void SpawnFire(Vector3 position, Vector3 scale, Quaternion rotation, int nrOfFlameTongues)
        {
            Instantiate(FirePrefab, position, rotation);

            if (nrOfFlameTongues <= 1) return;

            for (var i = 0; i < nrOfFlameTongues - 1; i++)
            {
                var xVaration = Random.value - VariationLimiter;

                var yVaration = Random.value - VariationLimiter;

                Instantiate(FirePrefab, new Vector3(position.x + xVaration, position.y + yVaration, position.z), rotation);
            }
        }


    }
}