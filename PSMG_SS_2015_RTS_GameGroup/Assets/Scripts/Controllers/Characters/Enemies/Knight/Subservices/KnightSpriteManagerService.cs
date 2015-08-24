using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightSpriteManagerService : MonoBehaviour
    {
        private SpriteRenderer knightTart;
        private Color defaultColor;
        public ParticleSystem Steam { get; private set; }
        private List<SpriteRenderer> sprites; 

        public void Awake()
        {
            knightTart =
                GetComponentsInChildren<SpriteRenderer>().First(sr => sr.gameObject.tag == TagReferences.KnightTart);

            HideTart();

            Steam = GetComponentInChildren<ParticleSystem>();
            Steam.Stop();

            sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
            defaultColor = sprites[0].color;
        }

        public void DisplayTart()
        {
            knightTart.enabled = true;
        }

        public void HideTart()
        {
            knightTart.enabled = false;
        }

        public void ColorKnightInRed()
        {
            sprites.ForEach(s => s.color = Color.red);
            Steam.Play();
        }

        public void ColorKnightInDefaultColor()
        {
            sprites.ForEach(s => s.color = defaultColor);
            Steam.Stop();
        }
    }
}