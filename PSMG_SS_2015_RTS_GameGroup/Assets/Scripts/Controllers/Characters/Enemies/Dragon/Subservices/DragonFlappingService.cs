using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonFlappingService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {

        private TriggerCollider2D flappingRange;
        private List<ImpController> impsInFlappingRange;

        public void Awake()
        {
            flappingRange =
                GetComponentsInChildren<TriggerCollider2D>()
                    .First(tc => tc.gameObject.tag == TagReferences.DragonFlappingRange);

            impsInFlappingRange = new List<ImpController>();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != flappingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerEnterImp(collider.GetComponent<ImpController>());
                    break;
            }
        }

        private void OnTriggerEnterImp(ImpController imp)
        {
            impsInFlappingRange.Add(imp);
            Flap();
        }

        private void Flap()
        {
            if (IsFlapping) return;
            IsFlapping = true;
            StartCoroutine(FlappingRoutine());
        }

        public bool IsFlapping { get; private set; }

        private IEnumerator FlappingRoutine()
        {
            // TODO
            

            yield return new WaitForSeconds(0f);

            IsFlapping = false;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != flappingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerExitImp(collider.GetComponent<ImpController>());
                    break;
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != flappingRange.GetInstanceID()) return;

            // TODO
        }

        private void OnTriggerExitImp(ImpController imp)
        {
            impsInFlappingRange.Remove(imp);
        }
    }

}