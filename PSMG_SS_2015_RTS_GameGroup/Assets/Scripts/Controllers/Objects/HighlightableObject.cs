using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class HighlightableObject : MonoBehaviour
    {
        private List<SpriteRenderer> spriteRenderers;

        private bool isHighlighted;
        private Counter blinkingCounter;

        private bool isBlinking;

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

        public bool IsBlinking
        {
            get
            {
                return isBlinking;
            }
            set
            {
                isBlinking = value;
                if (value) return;
                spriteRenderers.ForEach(sr => sr.color = defaultColor);
                isHighlighted = false;
            }
        }

        public void Start()
        {
            blinkingCounter = Counter.SetCounter(gameObject, 0.75f, Highlight, true);
        }

        private void Highlight()
        {
            if (!IsBlinking) return;

            if (!isHighlighted)
            {
                spriteRenderers.ForEach(sr => sr.color = Color.gray);
                isHighlighted = true;
            }
            else
            {
                spriteRenderers.ForEach(sr => sr.color = defaultColor);
                isHighlighted = false;
            }
        }
    }
}
