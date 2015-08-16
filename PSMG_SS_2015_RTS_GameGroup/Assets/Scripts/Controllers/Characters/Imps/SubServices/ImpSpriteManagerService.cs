using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpSpriteManagerService : MonoBehaviour
    {
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

        public void Awake()
        {
            Sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        public void MoveToSortingLayer(string sortingLayerName)
        {
            var spriteRenderers = Sprites.Where(sr => sr.sortingLayerName != SortingLayerReferences.Explosion).ToList();

            foreach (var sr in spriteRenderers)
            {
                sr.sortingLayerName = sortingLayerName;
                sr.sortingOrder += 10;
            }
        }

        public void MoveToDefaultSortingLayer()
        {
            var spriteRenderers = Sprites.Where(sr => sr.sortingLayerName != SortingLayerReferences.Explosion).ToList();

            foreach (var sr in spriteRenderers)
            {
                sr.sortingLayerName = SortingLayerReferences.Imp;
                sr.sortingOrder -= 10;
            }
        }

        public void MoveToSortingLayerPosition(int position)
        {
            foreach (var r in Sprites)
            {
                r.sortingOrder = position;
            }
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
                    s.sprite = SchwarzeneggerLeftUpperArm;
                }
                if (s.gameObject.name == "LeftLowerArm")
                {
                    leftLowerArmSprite = s.sprite;
                    s.sprite = SchwarzeneggerLeftLowerArm;
                }
                if (s.gameObject.name == "RightUpperArm")
                {
                    rightUpperArmSprite = s.sprite;
                    s.sprite = SchwarzeneggerRightLowerArm;
                }
                if (s.gameObject.name == "RightLowerArm")
                {
                    rightLowerArmSprite = s.sprite;
                    s.sprite = SchwarzeneggerRightLowerArm;
                }
                if (s.gameObject.name == "Body")
                {
                    bodySprite = s.sprite;
                    s.sprite = SchwarzeneggerBody;
                }
            }
            ToggleGlassesVisibility(true);
        }

        public void ToggleGlassesVisibility(bool isVisible)
        {
            var sprites = GetComponentsInChildren<SpriteRenderer>().ToList();

            foreach (var s in sprites.Where(s => s.gameObject.name == "LeftEyeGlasses" ||
                                                 s.gameObject.name == "RightEyeGlasses" ||
                                                 s.gameObject.name == "GlassesHandle"))
            {
                s.enabled = isVisible;
            }
        }
    }
}