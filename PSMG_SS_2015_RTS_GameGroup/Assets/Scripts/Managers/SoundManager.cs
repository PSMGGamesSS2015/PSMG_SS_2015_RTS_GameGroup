using Assets.Scripts.AssetReferences;
using Assets.Scripts.Config;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour, LevelManager.ILevelManagerListener
    {   
        AudioHelper backgroundMusic;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            backgroundMusic = gameObject.AddComponent<AudioHelper>();
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
        {
            backgroundMusic.Play(SoundReferences.MainTheme);
        }
    }
}