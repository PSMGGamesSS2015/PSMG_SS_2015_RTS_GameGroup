using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.General;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightAudioService : CharacterAudioService
    {
        public void PlaySelectionSound()
        {
            var randomLimit = SoundReferences.KnightSelectedVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.KnightSelectedVariants[randomNumber];
            Voice.Play(sound);
        }

        public void PlayDeathSound()
        {
            var randomLimit = SoundReferences.KnightDeadVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.KnightDeadVariants[randomNumber];
            Voice.Play(sound);
        }
    }
}