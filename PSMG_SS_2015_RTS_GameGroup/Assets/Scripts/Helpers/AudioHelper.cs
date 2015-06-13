using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class AudioHelper : MonoBehaviour
    {
        public AudioSource AudioSource { get; private set; }

        public void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Play(string clipName)
        {
            var audioClip = Resources.Load(clipName) as AudioClip;
            AudioSource.clip = audioClip;
            AudioSource.Play();
        }

    }
}