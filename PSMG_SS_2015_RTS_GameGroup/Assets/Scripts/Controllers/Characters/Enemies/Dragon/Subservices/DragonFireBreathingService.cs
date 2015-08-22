using System.Collections;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonFireBreathingService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D fireBreathingRange;
        public GameObject DragonFirePrefab;

        public void Awake()
        {
            fireBreathingRange =
                GetComponentsInChildren<TriggerCollider2D>()
                    .First(tc => tc.gameObject.tag == TagReferences.DragonFireBreathingRange);

            fireBreathingRange.RegisterListener(this);

            IsBreathingFire = false;
            IsFirstBreath = true;
            BreathingCounter = 0;
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

        public void BreathFire()
        {
            if (IsBreathingFire) return;

            if (IsFirstBreath) IsFirstBreath = false;

            BreathingCounter = 0;
            StartCoroutine(FireBreathingRoutine());
        }

        public bool IsBreathingFire { get; set; }
        public bool IsFirstBreath { get; private set; }
        public int BreathingCounter { get; set; }

        private IEnumerator FireBreathingRoutine()
        {
            IsBreathingFire = true;

            GetComponent<DragonAnimationHelper>().PlayBreathingAnimation();

            GetComponent<DragonMovementService>().StayInPosition();

            GetComponent<DragonSpriteManagerService>().HighlightNostrilsInColor(Color.red);

            yield return new WaitForSeconds(2.0f);

            InstantiateDragonFire();

            yield return new WaitForSeconds(1.0f);

            GetComponent<DragonAnimationHelper>().PlayFlyingAnimation();

            GetComponent<DragonMovementService>().Flydown();

            GetComponent<DragonSpriteManagerService>().HighlightNostrilsInDefaultColor();

            IsBreathingFire = false;
        }

        private void InstantiateDragonFire()
        {
            var posMiddle = gameObject.transform.position;
            var posMiddleLeft = new Vector3(gameObject.transform.position.x -1, gameObject.transform.position.y, gameObject.transform.position.z);
            var posOuterLeft = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
            var posMiddleRight = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            var posOuterRight = new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z);

            Instantiate(DragonFirePrefab, posMiddle, Quaternion.identity);
            Instantiate(DragonFirePrefab, posMiddleLeft, Quaternion.identity);
            Instantiate(DragonFirePrefab, posOuterLeft, Quaternion.identity);
            Instantiate(DragonFirePrefab, posMiddleRight, Quaternion.identity);
            Instantiate(DragonFirePrefab, posOuterRight, Quaternion.identity);
        }
    }
}