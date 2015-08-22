using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonSteamBreathingService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {

        private TriggerCollider2D steamBreathingRange;
        private List<ImpController> impsInBreathingRange;

        public void Awake()
        {
            steamBreathingRange =
                GetComponentsInChildren<TriggerCollider2D>()
                    .First(tc => tc.gameObject.tag == TagReferences.DragonSteamBreathingRange);
            
            steamBreathingRange.RegisterListener(this);

            impsInBreathingRange = new List<ImpController>();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != steamBreathingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerEnterImp(collider.GetComponent<ImpController>());
                    break;
            }
        }

        private void OnTriggerEnterImp(ImpController imp)
        {
            impsInBreathingRange.Add(imp);
            BreathSteam();
        }

        private void BreathSteam()
        {
            if (IsBreathingSteam) return;
            StartCoroutine(SteamBreathingRoutine());
        }

        public bool IsBreathingSteam { get; private set; }

        private IEnumerator SteamBreathingRoutine()
        {
            IsBreathingSteam = true;

            GetComponent<DragonAnimationHelper>().PlayBreathingAnimation();
            GetComponent<DragonMovementService>().Stand();

            yield return new WaitForSeconds(2.15f);

            // TODO Instantiate steam

            yield return new WaitForSeconds(0.85f);

            GetComponent<DragonAnimationHelper>().PlayFlyingAnimation();
            GetComponent<DragonMovementService>().Walk();

            IsBreathingSteam = false;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != steamBreathingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerExitImp(collider.GetComponent<ImpController>());
                    break;
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != steamBreathingRange.GetInstanceID()) return;

            // TODO
        }

        private void OnTriggerExitImp(ImpController imp)
        {
            impsInBreathingRange.Remove(imp);
        }
    }

}