using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour, LevelManager.LevelManagerListener
{   
    AudioSource backgroundMusic;
    
    public AudioClip mainTheme;
    public AudioClip backgroundTheme;
    public AudioClip levelWon;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        backgroundMusic = GetComponent<AudioSource>();
    }

    void LevelManager.LevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
    {
        backgroundMusic.clip = mainTheme;
        backgroundMusic.Play();
    }
}