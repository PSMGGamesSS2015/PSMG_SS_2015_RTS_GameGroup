using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.LevelScripts;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class GongController : MonoBehaviour
    {
        private AudioHelper audioHelper;

        public void Awake()
        {
            audioHelper = gameObject.AddComponent<AudioHelper>();
            audioHelper.AudioSource.volume = 0.7f;
        }

        public void Ring()
        {
            audioHelper.Play(SoundReferences.MetallDrum);


            if (LevelManager.Instance.CurrentLevel.CurrentLevelConfig.Name != SceneReferences.Level05CastleGlazeArrival) return;

            var events = (Level05Events) LevelManager.Instance.CurrentLevelEvents;
            events.DrawBridge.Lower();
        }
    }
}