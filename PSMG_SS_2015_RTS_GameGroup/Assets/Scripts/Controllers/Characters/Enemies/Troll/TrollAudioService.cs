using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.General;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollAudioService : CharacterAudioService
    {
        public void PlaySelectionSound()
        {
            var randomLimit = SoundReferences.TrollSelectedVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.TrollSelectedVariants[randomNumber];
            Voice.Play(sound);
        }

        public void PlayAttackSound()
        {
            var randomLimit = SoundReferences.TrollAttackVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.TrollAttackVariants[randomNumber];
            Voice.Play(sound);
        }
    }
}