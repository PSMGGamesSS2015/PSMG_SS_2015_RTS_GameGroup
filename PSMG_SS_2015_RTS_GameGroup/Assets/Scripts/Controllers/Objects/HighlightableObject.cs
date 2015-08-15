using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class HighlightableObject : MonoBehaviour
    {
        private List<SpriteRenderer> spriteRenderers;

        private bool isHighlighted;

        private Counter blinkingCounter;

        private Color defaultColor;

        public void Awake()
        {
            spriteRenderers = new List<SpriteRenderer>();

            if (GetComponent<SpriteRenderer>() != null)
            {
                spriteRenderers.Add(GetComponent<SpriteRenderer>());
            }
            GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(spriteRenderers.Add);

            defaultColor = spriteRenderers[0].color;

            isHighlighted = false;
        }


        public void Start()
        {
            blinkingCounter = Counter.SetCounter(gameObject, 0.75f, Highlight, true);
        }

        private void Highlight()
        {
            if (isHighlighted)
            {
                spriteRenderers.ForEach(sr => sr.color = Color.gray);
            }
            else
            {
                spriteRenderers.ForEach(sr => sr.color = defaultColor);
            }

            isHighlighted = !isHighlighted;
        }
    }
}
