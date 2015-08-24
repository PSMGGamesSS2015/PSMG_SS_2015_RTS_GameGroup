using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightCollisionSerivce : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D knightCollisionCheck;

        public void Awake()
        {
            knightCollisionCheck = GetComponentInChildren<TriggerCollider2D>();
            knightCollisionCheck.RegisterListener(this);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != knightCollisionCheck.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.TastyTart:
                    OnTriggerEnterTastyTart(collider);
                    break;
            }
        }

        private void OnTriggerEnterTastyTart(Collider2D collider)
        {
            GetComponent<KnightEatingTartService>().EatTart(collider.gameObject.GetComponent<TastyTartController>());
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != knightCollisionCheck.GetInstanceID()) return;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != knightCollisionCheck.GetInstanceID()) return;
        }
    }
}