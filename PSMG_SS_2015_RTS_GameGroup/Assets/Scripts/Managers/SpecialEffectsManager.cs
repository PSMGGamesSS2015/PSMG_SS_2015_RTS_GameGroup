using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SpecialEffectsManager : MonoBehaviour
    {
        public GameObject FirePrefab;

        public static SpecialEffectsManager Instance;

        public const float StandardScale = 0.1f;
        public const float StandardRotation = 0.1f;

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
            SpawnFire(new Vector3(position.x + 1f, position.y + 0.5f, position.z), new Vector3(StandardScale, StandardScale, StandardScale), Quaternion.identity);   
        }

        public void SpawnFire(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            Instantiate(FirePrefab, position, rotation);
        }


    }
}