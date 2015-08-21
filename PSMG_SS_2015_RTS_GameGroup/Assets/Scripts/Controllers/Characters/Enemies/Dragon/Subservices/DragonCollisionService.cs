using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonCollisionService : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == TagReferences.Imp) return;

            GetComponent<DragonMovementService>().ChangeDirection();
        }

    }
}