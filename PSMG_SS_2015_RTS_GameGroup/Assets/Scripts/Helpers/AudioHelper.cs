using UnityEngine;
using System.Collections;

public class AudioHelper : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string clipName)
    {
        AudioClip audioClip = Resources.Load(clipName) as AudioClip;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}