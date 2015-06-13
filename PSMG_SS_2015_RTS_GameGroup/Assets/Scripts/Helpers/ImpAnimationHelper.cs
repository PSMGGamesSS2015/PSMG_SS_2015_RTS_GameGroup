using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Types;

namespace Assets.Scripts.Helpers
{
    class ImpAnimationHelper : AnimationHelper
    {
        public ImpInventory ImpInventory;

        public override void Awake()
        {
            base.Awake();

            ImpInventory = GetComponentInChildren<ImpInventory>(); 
        }

        public void PlayTrainingAnimation(ImpType impType)
        {
            if (impType == ImpType.Coward)
            {
                ImpInventory.Display(TagReferences.ImpInventoryShield);
                Play(AnimationReferences.ImpHidingBehindShield);
            }
            if (impType == ImpType.Spearman)
            {
                ImpInventory.Display(TagReferences.ImpInventorySpear);
                Play(AnimationReferences.ImpWalkingSpear);
            }
            if (impType == ImpType.LadderCarrier)
            {
                ImpInventory.Display(TagReferences.ImpInventoryLadder);
                Play(AnimationReferences.ImpWalkingLadder);
            }
            if (impType == ImpType.Blaster)
            {
                ImpInventory.Display(TagReferences.ImpInventoryBomb);
                Play(AnimationReferences.ImpWalkingBomb);
            }
        }

        public void PlayActionAnimation(ImpType impType)
        {
            switch (impType)
            {
                case ImpType.Spearman:
                    break;
                case ImpType.Coward:
                    break;
                case ImpType.LadderCarrier:
                    break;
                case ImpType.Blaster:
                    break;
            }
        }

        public void PlayPlacingLadderHorizonallyAnimation()
        {
            ImpInventory.Display(TagReferences.ImpInventoryLadder);
            Play(AnimationReferences.ImpPlacingLadderHorizontally);
        }

        public void SwitchBackToStandardAnimation()
        {
            ImpInventory.HideItems();
            Play(AnimationReferences.ImpWalkingUnemployed);
        }

        public void PlayImpTakingObjectAnimation()
        {
            ImpInventory.HideItems();
            Play(AnimationReferences.ImpTakingObject);
        }

        public void PlayWalkingAnimation(ImpType type)
        {
            string anim;
            if (type == ImpType.Spearman)
            {
                anim = AnimationReferences.ImpWalkingSpear;
            }
            else
            {
                anim = AnimationReferences.ImpWalkingUnemployed;
            }
            Play(anim);
        }
    }
}
