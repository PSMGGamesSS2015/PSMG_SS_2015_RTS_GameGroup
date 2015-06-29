using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class SkyDecorationScript : MonoBehaviour
    {
        private List<GameObject> clouds;
        private float leftMarginOfSkyBox;
        private float rightMarginOfSkyBox;

        public void Awake()
        {
            clouds = GameObject.FindGameObjectsWithTag(TagReferences.Cloud).ToList();
            leftMarginOfSkyBox = gameObject.transform.position.x - gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
            rightMarginOfSkyBox = gameObject.transform.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
        }

        public void FixedUpdate()
        {
            clouds.ForEach(MoveCloud);
        }

        private void MoveCloud(GameObject cloud)
        {
            var pos = cloud.transform.position.x;
            pos = pos - 0.005f;
            if (pos + cloud.GetComponent<SpriteRenderer>().bounds.size.x <= leftMarginOfSkyBox)
            {
                pos = rightMarginOfSkyBox + cloud.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
            }
            cloud.transform.position = new Vector3(pos, cloud.transform.position.y, cloud.transform.position.z);
        }
    }
}