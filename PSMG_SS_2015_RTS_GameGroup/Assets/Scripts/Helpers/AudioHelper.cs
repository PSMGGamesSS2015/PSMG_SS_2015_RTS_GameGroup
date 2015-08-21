using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class AudioHelper : MonoBehaviour
    {
        public AudioSource AudioSource { get; private set; }
        private List<string> clipsInLine; 

        public void Awake()
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            clipsInLine = new List<string>();
        }

        public void Play(string clipName)
        {
            var audioClip = Resources.Load(clipName) as AudioClip;
            AudioSource.clip = audioClip;
            AudioSource.Play();
        }

        public void PlayAsLast(string clipName)
        {
            Play(clipName);

            clipsInLine.Clear();
        }

        public void PlayAfterCurrent(string clipName)
        {
            clipsInLine.Add(clipName);
        }

        public void Update()
        {
            if (AudioSource.isPlaying) return;
            if (clipsInLine.Count == 0) return;

            var nextClip = clipsInLine[0];
            clipsInLine.Remove(nextClip);
            clipsInLine.Sort();

            Play(nextClip);
        }
    }
}