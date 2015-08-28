using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.General;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonAudioService : CharacterAudioService
    {
        public void PlaySelectionSound()
        {
            var randomLimit = SoundReferences.DragonSelectedVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.DragonSelectedVariants[randomNumber];
            Voice.Play(sound);
        }
    }
}