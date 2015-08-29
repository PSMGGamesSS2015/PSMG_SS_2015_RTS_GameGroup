using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class SortingLayerHelper : MonoBehaviour
    {
        public string SortingLayerName;
        public int SortingLayerPosition;

        public void Awake()
        {
            var sortableObjects = GetComponentsInChildren<SpriteRenderer>().ToList();
            sortableObjects.ForEach(so => so.sortingLayerName = SortingLayerName);
            sortableObjects.ForEach(so => so.sortingLayerID = so.sortingLayerID + SortingLayerPosition);

            var particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();
            particleSystems.ForEach(ps => ps.GetComponent<Renderer>().sortingLayerName = SortingLayerName);
            particleSystems.ForEach(
                ps =>
                    ps.GetComponent<Renderer>().sortingOrder =
                        ps.GetComponent<Renderer>().sortingOrder + SortingLayerPosition);
        }
    }
}