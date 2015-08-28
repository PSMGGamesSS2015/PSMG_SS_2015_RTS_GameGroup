using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight
{
    public class KnightController : EnemyController
    {

        public bool IsLeaving { get; private set; }

        public void Awake()
        {
            IsLeaving = false;

            InitServices();
        }

        private void InitServices()
        {
            gameObject.AddComponent<KnightAnimationHelper>();
            gameObject.AddComponent<KnightMovementService>();
            gameObject.AddComponent<KnightAttackService>();
            gameObject.AddComponent<KnightSpriteManagerService>();
            gameObject.AddComponent<KnightCollisionSerivce>();
            gameObject.AddComponent<KnightEatingTartService>();
            gameObject.AddComponent<KnightFeelsSoHotService>();
            gameObject.AddComponent<KnightAudioService>();
            gameObject.AddComponent<KnightUIService>();
        }

        public void Leave()
        {
            if (IsLeaving) return;

            IsLeaving = true;
            StartCoroutine(LeavingRoutine());
        }

        private IEnumerator LeavingRoutine()
        {
            GetComponent<KnightMovementService>().Stand();
            GetComponent<KnightAnimationHelper>().Play(AnimationReferences.KnightDead);
            GetComponent<KnightAudioService>().PlayDeathSound();

            yield return new WaitForSeconds(1.5f);

            LeaveGame();
        }

        public void LeaveGame()
        {
            Destroy(gameObject);
        }
    }
}