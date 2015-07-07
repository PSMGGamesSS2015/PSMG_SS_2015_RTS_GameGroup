using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.Troll;
using Assets.Scripts.Helpers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpSpearmanService : ImpProfessionService, TriggerCollider2D.ITriggerCollider2DListener
    {
        private ImpAnimationHelper impAnimationService;
        private AudioHelper impAudioService;
        private ImpMovementService impMovementService;

        private Counter attackCounter;
        private TriggerCollider2D attackRange;
        private List<TrollController> enemiesInAttackRange;
        public ImpController CommandPartner;

        public void Awake()
        {
            InitComponents();
            InitTriggerCollider();
        }

        private void InitTriggerCollider()
        {
            var triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

            foreach (var c in triggerColliders)
            {
                if (c.tag == TagReferences.ImpAttackRange)
                {
                    attackRange = c;
                }
            }
            attackRange.RegisterListener(this);

            enemiesInAttackRange = new List<TrollController>();
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

            foreach (var enemy in enemiesInAttackRange)
            {
                enemy.GetComponent<TrollAttackService>().ReceiveHit();
            }
            enemiesInAttackRange.Clear();

            impAnimationService.Play(AnimationReferences.ImpStandingWithSpear);
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
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != GetComponent<ImpSpearmanService>().attackRange.GetInstanceID()) return;

            if (collider.gameObject.tag == TagReferences.EnemyTroll)
            {
                GetComponent<ImpSpearmanService>()
                    .enemiesInAttackRange.Add(collider.gameObject.GetComponent<TrollController>());
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
            // TODO
        }
    }
}