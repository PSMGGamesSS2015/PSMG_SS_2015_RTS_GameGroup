using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonSpriteManagerService : MonoBehaviour
    {
        private List<SpriteRenderer> nostrils;
        private List<SpriteRenderer> dragonSpriteRenderers;
        private Color defaultColor;

        public void Awake()
        {
            nostrils = new List<SpriteRenderer>();

            dragonSpriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        public void Start()
        {
            var dragonNostrils = GetComponentsInChildren<SpriteRenderer>().ToList().Where(sr => sr.tag == TagReferences.DragonNostril).ToList();

            dragonNostrils.ForEach(nostrils.Add);

            defaultColor = nostrils[0].color;
        }

        public void HighlightNostrilsInColor(Color color)
        {
            nostrils.ForEach(n => n.color = color);
        }

        public void HighlightNostrilsInDefaultColor()
        {
            nostrils.ForEach(n => n.color = defaultColor);
        }

        public void Blink()
        {
            StartCoroutine(BlinkingRoutine());
        }

        private void ColorDragonInRed()
        {
            dragonSpriteRenderers.ForEach(dsr => dsr.color = Color.red);
        }

        private void ColorDragonInStandardColor()
        {
            dragonSpriteRenderers.ForEach(dsr => dsr.color = defaultColor);
        }

        private IEnumerator BlinkingRoutine()
        {
            ColorDragonInRed();

            yield return new WaitForSeconds(0.5f);

            ColorDragonInStandardColor();

            yield return new WaitForSeconds(0.5f);

            ColorDragonInRed();

            yield return new WaitForSeconds(0.5f);

            ColorDragonInStandardColor();

            yield return new WaitForSeconds(0.5f);

            ColorDragonInRed();

            yield return new WaitForSeconds(0.5f);

            ColorDragonInStandardColor();
        }
    }
}