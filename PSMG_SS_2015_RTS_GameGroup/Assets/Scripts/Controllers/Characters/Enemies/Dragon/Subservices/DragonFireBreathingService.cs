using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonFireBreathingService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D fireBreathingRange;

        public void Awake()
        {
            fireBreathingRange =
                GetComponentsInChildren<TriggerCollider2D>()
                    .First(tc => tc.gameObject.tag == TagReferences.DragonFireBreathingRange);

            fireBreathingRange.RegisterListener(this);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != fireBreathingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerEnterImp();
                    break;
            }
        }

        private void OnTriggerEnterImp()
        {
            // TODO
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != fireBreathingRange.GetInstanceID()) return;

            // TODO
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != fireBreathingRange.GetInstanceID()) return;

            // TODO
        }
    }
}