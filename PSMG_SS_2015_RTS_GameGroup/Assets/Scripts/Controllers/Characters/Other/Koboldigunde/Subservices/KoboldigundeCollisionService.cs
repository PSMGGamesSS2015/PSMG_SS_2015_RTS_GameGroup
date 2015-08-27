using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Other.Koboldigunde.Subservices
{
    public class KoboldigundeCollisionService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D koboldigundeCollisionCheck;

        public void Awake()
        {
            koboldigundeCollisionCheck = GetComponentInChildren<TriggerCollider2D>();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != koboldigundeCollisionCheck.GetInstanceID()) return;

            throw new System.NotImplementedException();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != koboldigundeCollisionCheck.GetInstanceID()) return;

            throw new System.NotImplementedException();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != koboldigundeCollisionCheck.GetInstanceID()) return;

            throw new System.NotImplementedException();
        }
    }
}