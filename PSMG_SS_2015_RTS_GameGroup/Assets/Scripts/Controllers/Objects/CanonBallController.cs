using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.Knight;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class CanonBallController : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D canonBallCollisionCheck;

        public void Awake()
        {
            canonBallCollisionCheck = GetComponent<TriggerCollider2D>();
            canonBallCollisionCheck.RegisterListener(this);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case TagReferences.Knight:
                    OnTriggerEnterKnight(collider.GetComponent<KnightController>());
                    break;
            }
        }

        private void OnTriggerEnterKnight(KnightController knight)
        {
            // TODO Detonate
            // TODO hurt knight
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            // not needed
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            // not needed
        }
    }
}