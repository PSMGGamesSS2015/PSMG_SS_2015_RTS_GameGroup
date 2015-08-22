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
        private Color defaultColor;

        public void Awake()
        {
            nostrils = new List<SpriteRenderer>();
        }

        public void Start()
        {
            var leftNostril = GetComponentsInChildren<SpriteRenderer>().ToList().First(sr => sr.tag == TagReferences.LeftNostril);
            nostrils.Add(leftNostril);
            var rightNostril = GetComponentsInChildren<SpriteRenderer>().ToList().First(sr => sr.tag == TagReferences.RightNostril);
            nostrils.Add(rightNostril);

            defaultColor = leftNostril.color;
        }

        public void HighlightNostrilsInColor(Color color)
        {
            nostrils.ForEach(n => n.color = color);
        }

        public void HighlightNostrilsInDefaultColor()
        {
            nostrils.ForEach(n => n.color = defaultColor);
        }

    }
}