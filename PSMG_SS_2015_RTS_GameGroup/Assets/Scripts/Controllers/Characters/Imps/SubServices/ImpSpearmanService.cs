using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies;
using Assets.Scripts.Controllers.Characters.Enemies.Troll;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpSpearmanService : ImpProfessionService, TriggerCollider2D.ITriggerCollider2DListener
    {
        private const float AttackingDistanceToCloud = 2f;
        private Counter attackCounter;
        private TriggerCollider2D attackRange;
        public ImpController CommandPartner;
        private List<EnemyController> enemiesInAttackRange;
        private ImpAnimationHelper impAnimationService;
        private ImpMovementService impMovementService;

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != attackRange.GetInstanceID()) return;

            if (collider.gameObject.tag == TagReferences.EnemyTroll)
            {
                enemiesInAttackRange.Add(collider.gameObject.GetComponent<TrollController>());
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != attackRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.EnemyTroll:
                    OnTriggerExitTroll(collider.GetComponent<TrollController>());
                    break;
            }
        }

        private void OnTriggerExitTroll(TrollController trollController)
        {
            enemiesInAttackRange.Remove(trollController);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != attackRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.RainingCloud:
                    OnTriggerStayRainingCloud(collider.GetComponent<RainingCloudController>());
                    break;
                case TagReferences.EnemyTroll:
                    OnTriggerStayTroll(collider.GetComponent<TrollController>());
                    break;
            }
        }

        private void OnTriggerStayTroll(TrollController trollController)
        {
            if (enemiesInAttackRange.Contains(trollController)) return;
            if (trollController.IsLeaving) return;

            enemiesInAttackRange.Add(trollController);
        }

        private void OnTriggerStayRainingCloud(RainingCloudController rainingCloudController)
        {
            if (IsWithinStrikingDistance(rainingCloudController)) return; 
            if(rainingCloudController.IsAlreadyBeingAttacked) return;
            if(rainingCloudController.HasReceivedHit) return;

            PierceCloud(rainingCloudController);
        }

        public void Awake()
        {
            InitComponents();
            InitTriggerCollider();
        }

        private void InitTriggerCollider()
        {
            attackRange = GetComponentsInChildren<TriggerCollider2D>().First(c => c.tag == TagReferences.ImpAttackRange);
            attackRange.RegisterListener(this);
            enemiesInAttackRange = new List<EnemyController>();
        }

        private void InitComponents()
        {
            impAnimationService = GetComponent<ImpAnimationHelper>();
            impMovementService = GetComponent<ImpMovementService>();
        }

        private void Pierce()
        {
            StartCoroutine(PiercingRoutine());
        }

        private IEnumerator PiercingRoutine()
        {
            impAnimationService.Play(AnimationReferences.ImpAttackingWithSpear);
            GetComponent<ImpAudioService>().Sounds.Play(SoundReferences.ImpAttack1);

            yield return new WaitForSeconds(0.75f);

            enemiesInAttackRange.ForEach(Attack);

            impAnimationService.Play(AnimationReferences.ImpStandingWithSpear);
        }

        private void Attack(EnemyController enemyController)
        {
            if (enemyController.GetType() == typeof (TrollController))
            {
                AttackTroll((TrollController) enemyController);
            }
            else if (enemyController.GetType() == typeof (RainingCloudController))
            {
                AttackCloud((RainingCloudController) enemyController);
            }
        }

        private void AttackCloud(RainingCloudController rainingCloudController)
        {
            rainingCloudController.ReceiveHit();
            enemiesInAttackRange.Remove(rainingCloudController);
        }

        private void AttackTroll(TrollController trollController)
        {
            trollController.ReceiveHit();
            if (trollController.GetComponent<TrollMoodService>().IsAngry) enemiesInAttackRange.Remove(trollController);
        }

        public void FormCommand(ImpController commandPartner)
        {
            StandAndAttack();
            CommandPartner = commandPartner;
        }

        public void StandAndAttack()
        {
            impMovementService.Stand();
            impAnimationService.Play(AnimationReferences.ImpStandingWithSpear);
            attackCounter = Counter.SetCounter(this.gameObject, 4f, Pierce, true);
        }

        public void StopAttacking()
        {
            impMovementService.Walk();
            impAnimationService.Play(AnimationReferences.ImpWalkingSpear);

            if (attackCounter != null) attackCounter.Stop();
        }

        public void DissolveCommand()
        {
            StopAttacking();
            CommandPartner = null;
        }

        public bool IsInCommand()
        {
            return CommandPartner != null;
        }

        public void OnDestroy()
        {
            attackRange.UnregisterListener();
            enemiesInAttackRange.Clear();
        }

        private void PierceCloud(RainingCloudController rainingCloudController)
        {
            rainingCloudController.IsAlreadyBeingAttacked = true;
            enemiesInAttackRange.Add(rainingCloudController);
            impMovementService.Stand();
            impAnimationService.Play(AnimationReferences.ImpStandingWithSpear);
            attackCounter = Counter.SetCounter(this.gameObject, 4f, PierceOnce, false);
        }

        private bool IsWithinStrikingDistance(RainingCloudController rainingCloudController)
        {
            return Mathf.Abs(gameObject.transform.position.x - rainingCloudController.gameObject.transform.position.x) >
                   AttackingDistanceToCloud;
        }

        private void PierceOnce()
        {
            StartCoroutine(PiercingOnceRoutine());
        }

        private IEnumerator PiercingOnceRoutine()
        {
            impAnimationService.Play(AnimationReferences.ImpAttackingWithSpear);
            GetComponent<ImpAudioService>().Sounds.Play(SoundReferences.ImpAttack1);

            yield return new WaitForSeconds(0.75f);

            enemiesInAttackRange.ForEach(Attack);

            impAnimationService.PlayWalkingAnimation();
            impMovementService.Walk();
        }
    }
}