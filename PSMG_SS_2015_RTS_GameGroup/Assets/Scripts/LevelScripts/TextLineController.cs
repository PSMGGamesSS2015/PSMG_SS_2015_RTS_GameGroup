using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class TextLineController : MonoBehaviour
    {
        public SpriteRenderer Text { get; private set; }
        public SpriteRenderer LeftImage { get; private set; }
        public SpriteRenderer RightImage { get; private set; }

        public void Awake()
        {
            var spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();

            Text = spriteRenderers.First(sr => sr.gameObject.name == "Text");
            LeftImage = spriteRenderers.First(sr => sr.gameObject.name == "LeftImage");
            RightImage = spriteRenderers.First(sr => sr.gameObject.name == "RightImage");
        }
    }
}