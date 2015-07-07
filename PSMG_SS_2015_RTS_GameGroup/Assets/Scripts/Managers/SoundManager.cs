using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.ParameterObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour, LevelManager.ILevelManagerListener
    {   
        AudioHelper backgroundMusic;

        public static SoundManager Instance;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            DontDestroyOnLoad(gameObject);
            backgroundMusic = gameObject.AddComponent<AudioHelper>();
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            backgroundMusic.Play(SoundReferences.MainTheme);
        }
    }
}