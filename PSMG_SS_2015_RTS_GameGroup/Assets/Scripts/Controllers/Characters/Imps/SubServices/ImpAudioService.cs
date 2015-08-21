using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpAudioService : MonoBehaviour
    {
        public AudioHelper Voice { get; private set; }
        public AudioHelper Sounds { get; private set; }

        public void Awake()
        {
            Voice = gameObject.AddComponent<AudioHelper>();
            Sounds = gameObject.AddComponent<AudioHelper>();
        }

    }
}