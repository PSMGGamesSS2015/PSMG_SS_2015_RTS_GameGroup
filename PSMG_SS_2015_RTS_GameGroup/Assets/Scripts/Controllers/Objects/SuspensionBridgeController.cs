using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class SuspensionBridgeController : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {

        private List<ImpController> cowardsOnBridge;
        private TriggerCollider2D suspensionBridgeArea;
        private List<BreakableLink> breakableLinks;
        private List<BoxCollider2D> colliders;

        private const int NrOfCowardsThatBreakBridge = 3;

        public void Awake()
        {
            cowardsOnBridge = new List<ImpController>();
            suspensionBridgeArea = GetComponent<TriggerCollider2D>();
            suspensionBridgeArea.RegisterListener(this);
            breakableLinks = GetComponentsInChildren<BreakableLink>().ToList();
            colliders = GetComponentsInChildren<BoxCollider2D>().ToList();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != suspensionBridgeArea.GetInstanceID()) return;
            this.AddCowardToList(collider);
        }

        private void AddCowardToList(Collider2D collider)
        {
            if (collider.gameObject.tag != TagReferences.Imp) return;
            var imp = collider.gameObject.GetComponent<ImpController>();
            if (imp.GetComponent<ImpTrainingService>().Type != ImpType.Coward) return;
            if (cowardsOnBridge.Contains(imp)) return;
            cowardsOnBridge.Add(imp);
            CheckIfBridgeBreaks();
        }

        private void CheckIfBridgeBreaks()
        {
            if (cowardsOnBridge.Count >= NrOfCowardsThatBreakBridge)
            {
                Break();
            }
        }

        private void Break()
        {
            breakableLinks.ForEach(breakableLink => breakableLink.Break());
            colliders.ForEach(Destroy);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != suspensionBridgeArea.GetInstanceID()) return;
            RemoveCowardFromList(collider);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != suspensionBridgeArea.GetInstanceID()) return;
            AddCowardToList(collider);
        }

        private void RemoveCowardFromList(Collider2D collider)
        {
            if (collider.gameObject.tag != TagReferences.Imp) return;
            var imp = collider.gameObject.GetComponent<ImpController>();
            if (imp.GetComponent<ImpTrainingService>().Type != ImpType.Coward) return;
            cowardsOnBridge.Remove(imp);
        }
    }
}
