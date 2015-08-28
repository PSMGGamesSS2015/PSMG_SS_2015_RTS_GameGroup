using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollController : EnemyController
    {
        public EnemyType Type;

        private TrollAttackService attackService;
        private TrollMoodService moodService;

        public bool IsLeaving;

        public void Awake()
        {
            IsLeaving = false;
            attackService = gameObject.AddComponent<TrollAttackService>();
            moodService = gameObject.AddComponent<TrollMoodService>();
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
            GetComponent<AudioHelper>().Play(SoundReferences.TrollDeath);

            yield return new WaitForSeconds(2.15f);

            LeaveGame();
        }

        public void LeaveGame()
        {
            Destroy(gameObject);
        }

    }
}