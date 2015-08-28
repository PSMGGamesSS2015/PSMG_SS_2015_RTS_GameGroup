using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpAnimationHelper : AnimationHelper
    {
        public ImpInventory ImpInventory { get; private set; }

        public override void Awake()
        {
            base.Awake();
            ImpInventory = GetComponentInChildren<ImpInventory>();
            Play(AnimationReferences.ImpWalkingUnemployed);
        }

        public void PlayTrainingAnimation()
        {
            var impType = GetComponent<ImpTrainingService>().Type;

            switch (impType)
            {
                case ImpType.Spearman:
                    ImpInventory.Display(TagReferences.ImpInventorySpear);
                    Play(AnimationReferences.ImpWalkingSpear);
                    break;
                case ImpType.Coward:
                    ImpInventory.Display(TagReferences.ImpInventoryShield);
                    Play(AnimationReferences.ImpHidingBehindShield);
                    break;
                case ImpType.LadderCarrier:
                    ImpInventory.Display(TagReferences.ImpInventoryLadder);
                    Play(AnimationReferences.ImpWalkingLadder); 
                    break;
                case ImpType.Blaster:
                    ImpInventory.Display(TagReferences.ImpInventoryBomb);
                    Play(AnimationReferences.ImpWalkingBomb);
                    break;
                case ImpType.Firebug:
                    ImpInventory.TorchController.Display();
                    Play(AnimationReferences.ImpWalkingTorch);
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

        public void PlayWalkingAnimation()
        {
            var type = GetComponent<ImpTrainingService>().Type;

            string anim;

            switch (type)
            {
                case ImpType.Spearman:
                    anim = AnimationReferences.ImpWalkingSpear;
                    break;
                case ImpType.LadderCarrier:
                    anim = AnimationReferences.ImpWalkingLadder;
                    break;
                case ImpType.Firebug:
                    anim = AnimationReferences.ImpWalkingTorch;
                    break;
                default:
                    anim = AnimationReferences.ImpWalkingUnemployed;
                    break;
            }

            Play(anim);
        }

        public void PlayClimbingAnimation()
        {
            string anim;
            switch (GetComponent<ImpTrainingService>().Type)
            {
                case ImpType.Spearman:
                    anim = AnimationReferences.ImpClimbingLadderSpearman;
                    break;
                case ImpType.Unemployed:
                    anim = AnimationReferences.ImpClimbingLadderUnemployed;
                    break;
                case ImpType.Firebug:
                    anim = AnimationReferences.ImpClimbingLadderFirebug;
                    break;
                case ImpType.LadderCarrier:
                    anim = AnimationReferences.ImpClimbingLadderLadder;
                    break;
                default:
                    anim = AnimationReferences.ImpClimbingLadderUnemployed;
                    break;
            }

            GetComponent<ImpAnimationHelper>().Play(anim);
        }

        public void FlipExplosion()
        {
            ImpInventory.Explosion.Flip();
        }

        public void DisplayExplosion()
        {
            ImpInventory.DisplayExplosion();
        }

        public void PlayWinningAnimation()
        {
            GetComponent<ImpMovementService>().Stand();

            Play(AnimationReferences.ImpHappy);
        }
    }
}
