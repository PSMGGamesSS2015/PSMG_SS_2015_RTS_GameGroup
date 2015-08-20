using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollMoodService : MonoBehaviour {

        public bool IsAngry { get; set; }
        private List<SpriteRenderer> spriteRenderers;
        private Color originalColor;

        public void Awake()
        {
            IsAngry = false;
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            originalColor = spriteRenderers[0].color;
        }

        public void CalmDown()
        {
            IsAngry = false;
            SwitchBackToOriginalColor();
        }

        public void GetAngry()
        {
            IsAngry = true;
            spriteRenderers.ForEach(s => s.color = Color.red);
        }

        public void SwitchBackToOriginalColor()
        {
            spriteRenderers.ForEach(s => s.color = originalColor);
        }
    }
}