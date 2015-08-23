using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class BlackCoverController : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == TagReferences.Imp)
            {
                Destroy(gameObject);
            }
        }
    }
}