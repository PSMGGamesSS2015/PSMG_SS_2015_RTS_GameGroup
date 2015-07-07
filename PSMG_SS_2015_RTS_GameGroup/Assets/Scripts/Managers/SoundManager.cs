using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.ParameterObjects;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SoundManager : MonoBehaviour, LevelManager.ILevelManagerListener
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

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            BackgroundMusic.Play(SoundReferences.MainTheme);
        }
    }
}