using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class ImpOutOfSightIconController : MonoBehaviour
    {
        public ProfessionIconController IconDefault { get; private set; }
        public ProfessionIconController IconSpearman { get; private set; }
        public ProfessionIconController IconCoward { get; private set; }
        public ProfessionIconController IconLadderCarrier { get; private set; }
        public ProfessionIconController IconBlaster { get; private set; }
        public ProfessionIconController IconFirebug { get; private set; }
        public ProfessionIconController IconSchwarzenegger { get; private set; }

        public List<ProfessionIconController> ProfessionIcons { get; private set; }

        public PointerIconController PointerUp { get; private set; }
        public PointerIconController PointerDown { get; private set; }
        public PointerIconController PointerLeft { get; private set; }
        public PointerIconController PointerRight { get; private set; }

        public List<PointerIconController> PointerIcons { get; private set; }

        public void Awake()
        {
            InitProfessionIcons();
            InitPointers();
        }

        private void InitPointers()
        {
            PointerIcons = GetComponentsInChildren<PointerIconController>().ToList();

            PointerUp = PointerIcons.First(pi => pi.Direction == MovingObject.Direction.Upwards);
            PointerDown = PointerIcons.First(pi => pi.Direction == MovingObject.Direction.Downwards);
            PointerLeft = PointerIcons.First(pi => pi.Direction == MovingObject.Direction.Left);
            PointerRight = PointerIcons.First(pi => pi.Direction == MovingObject.Direction.Right);
        }

        private void InitProfessionIcons()
        {
            ProfessionIcons = GetComponentsInChildren<ProfessionIconController>().ToList();

            IconDefault = ProfessionIcons.First(pi => pi.ImpType == ImpType.Unemployed);
            IconSpearman = ProfessionIcons.First(pi => pi.ImpType == ImpType.Spearman);
            IconCoward = ProfessionIcons.First(pi => pi.ImpType == ImpType.Coward);
            IconLadderCarrier = ProfessionIcons.First(pi => pi.ImpType == ImpType.LadderCarrier);
            IconBlaster = ProfessionIcons.First(pi => pi.ImpType == ImpType.Blaster);
            IconFirebug = ProfessionIcons.First(pi => pi.ImpType == ImpType.Firebug);
            IconSchwarzenegger = ProfessionIcons.First(pi => pi.ImpType == ImpType.Schwarzenegger);
        }
    }
}