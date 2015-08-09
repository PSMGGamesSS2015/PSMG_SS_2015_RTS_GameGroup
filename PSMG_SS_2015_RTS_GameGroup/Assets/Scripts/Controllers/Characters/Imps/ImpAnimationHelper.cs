using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpAnimationHelper : AnimationHelper
    {
        private ImpInventory impInventory;

        public SpriteRenderer[] Sprites { get; private set; }

        public Sprite SchwarzeneggerRightLowerArm;
        public Sprite SchwarzeneggerRightUpperArm;
        public Sprite SchwarzeneggerLeftLowerArm;
        public Sprite SchwarzeneggerLeftUpperArm;
        public Sprite SchwarzeneggerBody;
        public Sprite SchwarzeneggerGlassesRight;
        public Sprite SchwarzeneggerGlassesLeft;
        public Sprite SchwarzeneggerGlassesHandle;

        private bool hasSchwarzeneggerSprites;

        private Sprite leftUpperArmSprite;
        private Sprite leftLowerArmSprite;
        private Sprite rightLowerArmSprite;
        private Sprite rightUpperArmSprite;
        private Sprite bodySprite;

        public override void Awake()
        {
            base.Awake();
            impInventory = GetComponentInChildren<ImpInventory>();
            Play(AnimationReferences.ImpWalkingUnemployed);
            Sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        public void MoveToSortingLayerPosition(int position)
        {
            foreach (var r in Sprites)
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
            if (impType == ImpType.Firebug)
            {
                impInventory.TorchController.Display();
                // TODO
            }
        }

        // TODO
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

        public void SwapSprites()
        {
            if (hasSchwarzeneggerSprites)
            {
                SwitchToStandardSprites();
                hasSchwarzeneggerSprites = false;
            }
            else
            {
                SwitchToSchwarzeneggerSprites();
                hasSchwarzeneggerSprites = true;
            }
        }
        
        // TODO Outsource

        private void SwitchToStandardSprites()
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>().ToList();

            foreach (var s in sprites)
            {
                if (s.gameObject.name == "LeftUpperArm")
                {
                    s.sprite = leftUpperArmSprite;
                }
                if (s.gameObject.name == "LeftLowerArm")
                {
                    s.sprite = leftLowerArmSprite;
                }
                if (s.gameObject.name == "RightUpperArm")
                {
                    s.sprite = rightUpperArmSprite;
                }
                if (s.gameObject.name == "RightLowerArm")
                {
                    s.sprite = rightLowerArmSprite;
                }
                if (s.gameObject.name == "Body")
                {
                    s.sprite = bodySprite;
                }
            }
            ToggleGlassesVisibility(false);
        }

        private void SwitchToSchwarzeneggerSprites()
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>().ToList();

            foreach (var s in sprites)
            {
                if (s.gameObject.name == "LeftUpperArm")
                {
                    leftUpperArmSprite = s.sprite;
                    s.sprite = GetComponent<ImpAnimationHelper>().SchwarzeneggerLeftUpperArm;
                }
                if (s.gameObject.name == "LeftLowerArm")
                {
                    leftLowerArmSprite = s.sprite;
                    s.sprite = GetComponent<ImpAnimationHelper>().SchwarzeneggerLeftLowerArm;
                }
                if (s.gameObject.name == "RightUpperArm")
                {
                    rightUpperArmSprite = s.sprite;
                    s.sprite = GetComponent<ImpAnimationHelper>().SchwarzeneggerRightLowerArm;
                }
                if (s.gameObject.name == "RightLowerArm")
                {
                    rightLowerArmSprite = s.sprite;
                    s.sprite = GetComponent<ImpAnimationHelper>().SchwarzeneggerRightLowerArm;
                }
                if (s.gameObject.name == "Body")
                {
                    bodySprite = s.sprite;
                    s.sprite = GetComponent<ImpAnimationHelper>().SchwarzeneggerBody;
                }
            }
            ToggleGlassesVisibility(true);
        }

        public void ToggleGlassesVisibility(bool isVisible)
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
            foreach (var s in sprites)
            {
                if (s.gameObject.name == "LeftEyeGlasses")
                {
                    s.enabled = isVisible;
                }
                if (s.gameObject.name == "RightEyeGlasses")
                {
                    s.enabled = isVisible;
                }
                if (s.gameObject.name == "GlassesHandle")
                {
                    s.enabled = isVisible;
                }
            }

        }
    }
}
