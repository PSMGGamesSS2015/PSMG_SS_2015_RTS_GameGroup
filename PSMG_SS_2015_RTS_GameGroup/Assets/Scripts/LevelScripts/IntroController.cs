using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class IntroController : MonoBehaviour
    {

        private SpriteRenderer kruemelbartImage;

        private SpriteRenderer koboldigundeImage;

        private SpriteRenderer cakeImage;

        private SpriteRenderer impImage;

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
            Counter.SetCounter(gameObject, 15f, ChangeAudioVolume, false);

            Counter.SetCounter(gameObject, 24f, DisplayKoboldigunde, false);

            Counter.SetCounter(gameObject, 42f, DisplayKruemelbart, false);

            Counter.SetCounter(gameObject, 62f, DisplayImp, false);

            Counter.SetCounter(gameObject, 62f, DisplayCake, false);

            Counter.SetCounter(gameObject, 80f, LoadNextLevel, false);
        }

        private void ChangeAudioVolume()
        {
            SoundManager.Instance.BackgroundMusic.AudioSource.volume = 0.1f;
        }

        private void LoadNextLevel()
        {
            LevelManager.Instance.LoadNextLevel();
        }

        private void DisplayCake()
        {
            cakeImage.sprite = CakeSprite;
            kruemelbartImage.enabled = false;
            koboldigundeImage.enabled = false;
        }

        private void DisplayKoboldigunde()
        {
            koboldigundeImage.sprite = KoboldigundeSprite;
        }

        private void DisplayKruemelbart()
        {
            kruemelbartImage.sprite = KruemelbartSprite;
        }

        private void DisplayImp()
        {
            impImage.sprite = ImpSprite;
        }

        private void InitComponents()
        {
            koboldigundeImage = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "KoboldigundeImage");

            kruemelbartImage = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "KruemelbartImage");

            cakeImage = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "CakeImage");

            impImage = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "ImpImage");
        }
    }
}