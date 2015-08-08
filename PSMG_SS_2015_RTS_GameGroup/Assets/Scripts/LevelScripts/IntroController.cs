using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class IntroController : MonoBehaviour
    {

        private SpriteRenderer leftImage;

        private SpriteRenderer rightImage;

        public Sprite KoboldigundeSprite;

        public Sprite KruemelbartSprite;

        public Sprite ImpSprite;

        public Sprite CakeSprite;

        public void Awake()
        {
            InitComponents();

            SetCounters();
        }

        private void SetCounters()
        {
            Counter.SetCounter(gameObject, 20f, DisplayKoboldigunde, false);

            Counter.SetCounter(gameObject, 38f, DisplayKruemelbart, false);

            Counter.SetCounter(gameObject, 62f, DisplayImp, false);
        }

        private void DisplayKoboldigunde()
        {
            rightImage.sprite = KoboldigundeSprite;
        }

        private void DisplayKruemelbart()
        {
            leftImage.sprite = KruemelbartSprite;
        }

        private void DisplayImp()
        {
            rightImage.sprite = ImpSprite;
        }

        private void InitComponents()
        {
            leftImage = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "LeftImage");

            rightImage = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "RightImage");
        }
    }
}