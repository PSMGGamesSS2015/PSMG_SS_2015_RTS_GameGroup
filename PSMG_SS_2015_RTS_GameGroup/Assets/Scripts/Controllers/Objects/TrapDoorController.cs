using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class TrapDoorController : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private const string LeftPartName = "LeftPart";
        private const string RightPartName = "RightPart";

        private GameObject leftPart;
        private HingeJoint2D leftJointToLoosen;
        private Collider2D leftPartCollider;

        private GameObject rightPart;
        private HingeJoint2D rightJointToLoosen;
        private Collider2D rightPartCollider;

        private List<ImpController> impsOnTrapDoors;
        private TriggerCollider2D trapDoorCollisionCheck;

        private bool isOpen;

        public void Awake()
        {
            var parts = GetComponentsInChildren<SpriteRenderer>().ToList();

            leftPart = parts.First(sr => sr.name == LeftPartName).gameObject;
            leftJointToLoosen = leftPart.GetComponents<HingeJoint2D>().First(hj => hj.useLimits == false);
            leftPartCollider = leftPart.GetComponent<Collider2D>();

            rightPart = parts.First(sr => sr.name == RightPartName).gameObject;
            rightJointToLoosen = rightPart.GetComponents<HingeJoint2D>().First(hj => hj.useLimits == false);
            rightPartCollider = rightPart.GetComponent<Collider2D>();

            impsOnTrapDoors = new List<ImpController>();
            trapDoorCollisionCheck = GetComponent<TriggerCollider2D>();
            trapDoorCollisionCheck.RegisterListener(this);

            isOpen = false;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != trapDoorCollisionCheck.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerEnterImp(collider.GetComponent<ImpController>());
                    break;
            }
        }

        private void OnTriggerEnterImp(ImpController imp)
        {
            if (isOpen) return;
            if (impsOnTrapDoors.Contains(imp)) return;

            impsOnTrapDoors.Add(imp);
            CheckIfDoorOpens();
        }

        private void CheckIfDoorOpens()
        {
            if (impsOnTrapDoors.Count < 2) return;
            isOpen = true;

            leftJointToLoosen.enabled = false;
            leftPartCollider.enabled = false;

            rightJointToLoosen.enabled = false;
            rightPartCollider.enabled = false;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != trapDoorCollisionCheck.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerExitImp(collider.GetComponent<ImpController>());
                    break;
            }
        }

        private void OnTriggerExitImp(ImpController imp)
        {
            if (isOpen) return;
            if (!impsOnTrapDoors.Contains(imp)) return;

            impsOnTrapDoors.Remove(imp);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != trapDoorCollisionCheck.GetInstanceID())
            {
                
            }
        }
    }
}