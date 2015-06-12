using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class RavineController : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collider)
        {
            var tag = collider.gameObject.tag;

            if (tag == TagReferences.Imp)
            {
                collider.gameObject.GetComponent<ImpController>().LeaveGame();
            }
        }

    }
}