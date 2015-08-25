using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightEatingTartService : MonoBehaviour
    {
        public bool IsEatingTart { get; private set; }

        public void Awake()
        {
            IsEatingTart = false;
        }

        public void EatTart(TastyTartController tart)
        {
            if (IsEatingTart) return;

            GetComponent<KnightAudioService>().Voice.Play(SoundReferences.SoundLvl6_05_KnightEatingCake);

            IsEatingTart = true;

            GetComponent<KnightMovementService>().Stand();
            GetComponent<KnightAnimationHelper>().Play(AnimationReferences.KnightEating);
            Counter.SetCounter(gameObject, 2.5f, PlaySaliveSound, true);

            GetComponent<KnightSpriteManagerService>().DisplayTart();

            Destroy(tart.gameObject);
        }

        private void PlaySaliveSound()
        {
            GetComponent<KnightAudioService>().Voice.Play(SoundReferences.SoundLvl6_04_KnightSaliva);
        }
    }
}