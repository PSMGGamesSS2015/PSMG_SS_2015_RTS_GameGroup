using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.General;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpAudioService : CharacterAudioService
    {
        public void PlaySelectionSound()
        {
            var randomLimit = SoundReferences.ImpSelectedVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.ImpSelectedVariants[randomNumber];
            Voice.Play(sound);
        }
    }
}