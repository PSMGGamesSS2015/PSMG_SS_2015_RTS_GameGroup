using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.BlueTroll.Subservices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.BlueTroll
{
    public class BlueTrollController : EnemyController
    {
        public bool IsLeaving;

        private BlueTrollMoodService moodService;
        private BlueTrollAttackService attackService;
        private BlueTrollAnimationHelper animationHelper;
        private BlueTrollAudioService audioService;

        public void Awake()
        {
            animationHelper = gameObject.AddComponent<BlueTrollAnimationHelper>();
            attackService = gameObject.AddComponent<BlueTrollAttackService>();
            moodService = gameObject.AddComponent<BlueTrollMoodService>();
            audioService = gameObject.AddComponent<BlueTrollAudioService>();

            IsLeaving = false;
        }

        public void ReceiveHit()
        {
            if (!moodService.IsAngry)
            {
                moodService.GetAngry();
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
                attackService.StrikeAtOnce();
            }
            else
            {
                IsLeaving = true;
                StopCoroutine(attackService.SmashingRoutine());
                animationHelper.Play(AnimationReferences.BlueTrollStanding);
                StartCoroutine(LeavingRoutine());
            }

        }

        private IEnumerator LeavingRoutine()
        {
            moodService.SwitchBackToOriginalColor();
            attackService.StopCounters();

            animationHelper.Play(AnimationReferences.BlueTrollDead);
            audioService.Voice.PlayAsLast(SoundReferences.TrollDeath);

            yield return new WaitForSeconds(2.15f);

            LeaveGame();
        }

        public void LeaveGame()
        {
            Destroy(gameObject);
        }
    }
}