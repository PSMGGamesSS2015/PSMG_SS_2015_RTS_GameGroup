using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpAnimationHelper : AnimationHelper
    {
        private ImpInventory impInventory;

        public override void Awake()
        {
            base.Awake();
            impInventory = GetComponentInChildren<ImpInventory>();
            Play(AnimationReferences.ImpWalkingUnemployed);
        }

        public void PlayTrainingAnimation()
        {
            var impType = GetComponent<ImpTrainingService>().Type;

            switch (impType)
            {
                case ImpType.Spearman:
                    impInventory.Display(TagReferences.ImpInventorySpear);
                    Play(AnimationReferences.ImpWalkingSpear);
                    break;
                case ImpType.Coward:
                    impInventory.Display(TagReferences.ImpInventoryShield);
                    Play(AnimationReferences.ImpHidingBehindShield);
                    break;
                case ImpType.LadderCarrier:
                    impInventory.Display(TagReferences.ImpInventoryLadder);
                    Play(AnimationReferences.ImpWalkingLadder); 
                    break;
                case ImpType.Blaster:
                    impInventory.Display(TagReferences.ImpInventoryBomb);
                    Play(AnimationReferences.ImpWalkingBomb);
                    break;
                case ImpType.Firebug:
                    impInventory.TorchController.Display();
                    Play(AnimationReferences.ImpWalkingTorch);
                    break;
            }
        }

        public void PlayPlacingLadderHorizonallyAnimation()
        {
            impInventory.Display(TagReferences.ImpInventoryLadder);
            Play(AnimationReferences.ImpPlacingLadderHorizontally);
        }

        public void SwitchBackToStandardAnimation()
        {
            impInventory.HideItems();
            Play(AnimationReferences.ImpWalkingUnemployed);
        }

        public void PlayImpTakingObjectAnimation()
        {
            impInventory.HideItems();
            Play(AnimationReferences.ImpTakingObject);
        }

        public void PlayWalkingAnimation()
        {
            var type = GetComponent<ImpTrainingService>().Type;

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

        public void FlipExplosion()
        {
            impInventory.Explosion.Flip();
        }

        public void DisplayExplosion()
        {
            impInventory.DisplayExplosion();
        }
    }
}
