using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour, LevelManager.LevelManagerListener
{   
    AudioHelper backgroundMusic;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        backgroundMusic = gameObject.AddComponent<AudioHelper>();
    }

    void LevelManager.LevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
    {
        backgroundMusic.Play(SoundReferences.MAIN_THEME);
    }
}