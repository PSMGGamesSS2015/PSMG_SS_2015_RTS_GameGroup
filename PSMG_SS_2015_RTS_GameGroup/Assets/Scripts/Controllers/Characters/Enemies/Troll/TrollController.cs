using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollController : EnemyController
    {

        private TrollAttackService attackService;
        private TrollMoodService moodService;
        private TrollAudioService audioService;

        public bool IsLeaving;

        public void Awake()
        {
            IsLeaving = false;
            attackService = gameObject.AddComponent<TrollAttackService>();
            moodService = gameObject.AddComponent<TrollMoodService>();
            audioService = gameObject.AddComponent<TrollAudioService>();
            gameObject.AddComponent<TrollUIService>();
        }

        public void ReceiveHit()
        {
            if (!moodService.IsAngry)
            {
                GetComponent<TrollMoodService>().GetAngry();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
                attackService.StrikeAtOnce();
            }
            else
            {
                IsLeaving = true;
                StopCoroutine(attackService.SmashingRoutine());
                GetComponent<AnimationHelper>().Play(AnimationReferences.TrollStanding);
                StartCoroutine(LeavingRoutine());
            }

        }

        private IEnumerator LeavingRoutine()
        {
            moodService.SwitchBackToOriginalColor();
            attackService.StopCounters();

            GetComponent<AnimationHelper>().Play(AnimationReferences.TrollDead);
            audioService.Voice.Play(SoundReferences.TrollDeath);

            yield return new WaitForSeconds(2.15f);

            LeaveGame();
        }

        public void LeaveGame()
        {
            Destroy(gameObject);
        }

    }
}