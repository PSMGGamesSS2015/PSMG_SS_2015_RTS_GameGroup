using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies;
using Assets.Scripts.Controllers.Characters.Enemies.Troll;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Helpers;
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
        private AudioHelper impAudioService;
        private ImpMovementService impMovementService;

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != GetComponent<ImpSpearmanService>().attackRange.GetInstanceID()) return;

            if (collider.gameObject.tag == TagReferences.EnemyTroll)
            {
                enemiesInAttackRange.Add(collider.gameObject.GetComponent<TrollController>());
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != attackRange.GetInstanceID()) return;

            if (collider.gameObject.tag == TagReferences.EnemyTroll)
            {
                enemiesInAttackRange.Remove(collider.gameObject.GetComponent<TrollController>());
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != GetComponent<ImpSpearmanService>().attackRange.GetInstanceID()) return;

            if (collider.gameObject.tag == TagReferences.RainingCloud)
            {
                var rainingCloudController = collider.gameObject.GetComponent<RainingCloudController>();

                if (IsWithinStrikingDistance(rainingCloudController) || rainingCloudController.IsAlreadyBeingAttacked ||
                    rainingCloudController.HasReceivedHit) return;

                PierceCloud(rainingCloudController);
            }
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
            impAudioService = GetComponent<AudioHelper>();
            impMovementService = GetComponent<ImpMovementService>();
        }

        private void Pierce()
        {
            StartCoroutine(PiercingRoutine());
        }

        private IEnumerator PiercingRoutine()
        {
            impAnimationService.Play(AnimationReferences.ImpAttackingWithSpear);
            impAudioService.Play(SoundReferences.ImpAttack1);

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
            if (trollController.GetComponent<TrollAttackService>().IsAngry)
            {
                trollController.GetComponent<TrollAttackService>().ReceiveHit();
                enemiesInAttackRange.Remove(trollController);
            }
            else
            {
                trollController.GetComponent<TrollAttackService>().ReceiveHit();
            }
        }

        public void FormCommand(ImpController commandPartner)
        {
            impMovementService.Stand();
            impAnimationService.Play(AnimationReferences.ImpStandingWithSpear);
            attackCounter = Counter.SetCounter(this.gameObject, 4f, Pierce, true);

            CommandPartner = commandPartner;
        }

        public void DissolveCommand()
        {
            impMovementService.Walk();
            impAnimationService.Play(AnimationReferences.ImpWalkingSpear);

            if (attackCounter != null)
            {
                attackCounter.Stop();
            }

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
            impAudioService.Play(SoundReferences.ImpAttack1);

            yield return new WaitForSeconds(0.75f);

            enemiesInAttackRange.ForEach(Attack);

            impAnimationService.PlayWalkingAnimation();
            impMovementService.Walk();
        }
    }
}