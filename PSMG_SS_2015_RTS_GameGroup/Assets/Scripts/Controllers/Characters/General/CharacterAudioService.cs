﻿using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.General
{
    public class CharacterAudioService : MonoBehaviour
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
    }
}