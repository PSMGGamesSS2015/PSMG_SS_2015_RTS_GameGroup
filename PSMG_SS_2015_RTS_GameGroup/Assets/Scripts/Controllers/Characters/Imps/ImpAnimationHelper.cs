using Assets.Scripts.AssetReferences;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpAnimationHelper : AnimationHelper
    {
        private ImpInventory impInventory;
        private SpriteRenderer[] sprites;

        public override void Awake()
        {
            base.Awake();
            impInventory = GetComponentInChildren<ImpInventory>();
            Play(AnimationReferences.ImpWalkingUnemployed);
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        public void MoveToSortingLayerPosition(int position)
        {
            foreach (var r in sprites)
            {
                r.sortingOrder = position;
            }
        }

        public void PlayTrainingAnimation(ImpType impType)
        {
            if (impType == ImpType.Coward)
            {
                impInventory.Display(TagReferences.ImpInventoryShield);
                Play(AnimationReferences.ImpHidingBehindShield);
            }
            if (impType == ImpType.Spearman)
            {
                impInventory.Display(TagReferences.ImpInventorySpear);
                Play(AnimationReferences.ImpWalkingSpear);
            }
            if (impType == ImpType.LadderCarrier)
            {
                impInventory.Display(TagReferences.ImpInventoryLadder);
                Play(AnimationReferences.ImpWalkingLadder);
            }
            if (impType == ImpType.Blaster)
            {
                impInventory.Display(TagReferences.ImpInventoryBomb);
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
