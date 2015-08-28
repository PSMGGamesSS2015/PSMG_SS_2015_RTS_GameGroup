using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class GongController : MonoBehaviour
    {
        private AudioHelper audioHelper;

        public void Awake()
        {
            audioHelper = gameObject.AddComponent<AudioHelper>();
            audioHelper.AudioSource.volume = 0.7f;
        }

        public void Ring()
        {
            audioHelper.Play(SoundReferences.MetallDrum);
        }
    }
}