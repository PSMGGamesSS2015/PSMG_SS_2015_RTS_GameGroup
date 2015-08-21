using Assets.Scripts.Helpers;
using Assets.Scripts.ParameterObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour, LevelManager.ILevelManagerListener,
        LevelManager.ILevelManagerMenuSceneListener, LevelManager.ILevelManagerNarrativeSceneListener
    {
        public AudioHelper BackgroundMusic { get; private set; }
        public AudioHelper Narrator { get; private set; }

        private int indexOfCurrentClipInPlaylist;

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
            BackgroundMusic.AudioSource.volume = 0.3f;
            Narrator = gameObject.AddComponent<AudioHelper>();
        }

        public void Update()
        {
            CheckIfMusicIsRunning();
        }

        private void CheckIfMusicIsRunning()
        {
            if (BackgroundMusic.AudioSource.isPlaying || LevelManager.Instance.CurrentLevel == null) return;
            
            var playList = LevelManager.Instance.CurrentLevel.CurrentLevelConfig.PlayList;

            if (indexOfCurrentClipInPlaylist < playList.Length - 1)
            {
                indexOfCurrentClipInPlaylist = indexOfCurrentClipInPlaylist + 1;
            }
            else
            {
                indexOfCurrentClipInPlaylist = 0;
            }
            // play next clip
            BackgroundMusic.Play(playList[indexOfCurrentClipInPlaylist]);
        }

        private void StartPlaylist(Level level)
        {
            BackgroundMusic.Play(level.CurrentLevelConfig.PlayList[0]);
            indexOfCurrentClipInPlaylist = 0;
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            StartPlaylist(level);
        }

        void LevelManager.ILevelManagerMenuSceneListener.OnMenuLevelStarted(Level level)
        {
            StartPlaylist(level);
        }

        void LevelManager.ILevelManagerNarrativeSceneListener.OnNarrativeLevelStarted(Level level)
        {
            StartPlaylist(level);
        }
    }
}