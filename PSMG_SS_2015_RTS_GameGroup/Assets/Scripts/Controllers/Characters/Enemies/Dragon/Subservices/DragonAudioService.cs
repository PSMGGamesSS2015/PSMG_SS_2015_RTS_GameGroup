using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonAudioService : MonoBehaviour
    {
        public AudioHelper Voice { get; private set; }
        public AudioHelper Sounds { get; private set; }

        public void Awake()
        {
            Voice = gameObject.AddComponent<AudioHelper>();
            Voice.AudioSource.volume = 0.7f;
            Sounds = gameObject.AddComponent<AudioHelper>();
            Sounds.AudioSource.volume = 0.7f;
        }

        public void PlaySelectionSound()
        {
            var randomLimit = SoundReferences.DragonSelectedVariants.Length;
            var randomNumber = Random.Range(0, randomLimit);
            var sound = SoundReferences.DragonSelectedVariants[randomNumber];
            Voice.Play(sound);
        }
    }
}