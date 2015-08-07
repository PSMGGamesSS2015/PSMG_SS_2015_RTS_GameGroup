using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.ParameterObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour, LevelManager.ILevelManagerListener, LevelManager.ILevelManagerMenuSceneListener
    {
        public AudioHelper BackgroundMusic { get; private set; }

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
            BackgroundMusic = gameObject.AddComponent<AudioHelper>();
        }

        public void Update()
        {
            if (!BackgroundMusic.AudioSource.isPlaying)
            {
                BackgroundMusic.PlayNextClip();
            }
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            BackgroundMusic.Play(level.CurrentLevelConfig.PlayList[0]);
        }

        void LevelManager.ILevelManagerMenuSceneListener.OnMenuLevelStarted(Level level)
        {
            BackgroundMusic.Play(level.CurrentLevelConfig.PlayList[0]);
        }
    }
}