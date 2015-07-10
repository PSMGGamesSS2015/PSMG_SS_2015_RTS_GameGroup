using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollAttackService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D triggerCollider2D;
        private List<ImpController> impsInAttackRange;
        private bool isSmashing;

        public bool IsAngry { get; private set; }

        private Counter hitDelayCounter;
        private Counter angryCounter;
        private bool isLeaving;

        public void Awake()
        {
            triggerCollider2D = GetComponentInChildren<TriggerCollider2D>();
            
            impsInAttackRange = new List<ImpController>();
        }

        public void Start()
        {
            triggerCollider2D.RegisterListener(this);
            IsAngry = false;
            isLeaving = false;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != triggerCollider2D.GetInstanceID()) return; // check if the triggered collider is attack range
            if (collider.gameObject.tag != TagReferences.Imp) return; // check if an imp enters the troll's attack range
            if (isSmashing) return; // check if the troll is striking with his weapon at this very moment

            impsInAttackRange.Add(collider.gameObject.GetComponent<ImpController>());

            if (IsAngry) // if the troll is angry he attacks as soon as an imp enters his field of sight
            {
                StrikeWithMaul();
            }
            else
            {
                if (hitDelayCounter != null) return;
                hitDelayCounter = Counter.SetCounter(gameObject, 2.0f, StrikeWithMaul, true);
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (isSmashing) return;
            if (collider.gameObject.tag != TagReferences.Imp) return;

            if (!impsInAttackRange.Contains(collider.gameObject.GetComponent<ImpController>())) return;
            
            impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());
            
            if (impsInAttackRange.Count != 0) return;
            
            if (hitDelayCounter != null) // stop the counter when no more imps are in a trolls attack range
            {
                hitDelayCounter.Stop();
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            // not needed here
        }

        private void StrikeWithMaul()
        {
            StartCoroutine(SmashingRoutine());
        }

        private void SmashImpsBetweenCowardAndTroll(ImpController coward)
        {
            var distanceBetweenCowardAndTroll = Vector2.Distance(gameObject.transform.position, coward.gameObject.transform.position);

            var impsToBeHit = (from imp in impsInAttackRange let currentDistance = Vector2.Distance(gameObject.transform.position, imp.gameObject.transform.position) where currentDistance < distanceBetweenCowardAndTroll select imp).ToList();

            foreach (var imp in impsToBeHit)
            {
                impsInAttackRange.Remove(imp);
                imp.LeaveGame(); // actually hit the imps
            }

            if (impsInAttackRange.Count != 0) return;

            if (hitDelayCounter != null) // stop hitting if no more imps are in attack range
            {
                hitDelayCounter.Stop();
            }
        }

        private ImpController SearchForCoward()
        {
            return impsInAttackRange.FirstOrDefault(imp => imp.GetComponent<ImpTrainingService>().Type == ImpType.Coward);
        }

        private void SmashAllImpsInRange()
        {
            for (var i = impsInAttackRange.Count - 1; i >= 0; i--)
            {
                impsInAttackRange[i].LeaveGame();
            }

            impsInAttackRange.Clear();

            if (hitDelayCounter != null)
            {
                hitDelayCounter.Stop();
            }
        }

        public IEnumerator SmashingRoutine()
        {
            GetComponent<AudioHelper>().Play(SoundReferences.TrollAttack2);
            GetComponent<AnimationHelper>().Play(AnimationReferences.TrollAttacking);

            yield return new WaitForSeconds(1f);

            if (!isLeaving)
            {
                
                var coward = SearchForCoward(); // check if there is a coward within striking distance

                isSmashing = true;

                if (coward != null)
                {
                    SmashImpsBetweenCowardAndTroll(coward);
                }
                else
                {
                    SmashAllImpsInRange();
                }
                if (!isLeaving)
                {
                    GetComponent<AnimationHelper>().Play(AnimationReferences.TrollStanding);
                }

            }
            isSmashing = false;
        }

        public void ReceiveHit()
        {
            if (!IsAngry)
            {
                IsAngry = true;
                hitDelayCounter.Stop();
                StrikeWithMaul();
                angryCounter = Counter.SetCounter(gameObject, 10f, CalmDown, false);
            }
            else
            {
                isLeaving = true;
                StopCoroutine(GetComponent<TrollAttackService>().SmashingRoutine());
                GetComponent<AnimationHelper>().Play(AnimationReferences.TrollStanding);
                StartCoroutine(LeavingRoutine());
            }
            
        }

        private void CalmDown()
        {
            IsAngry = false;
        }

        private IEnumerator LeavingRoutine()
        {
            if (hitDelayCounter != null)
            {
                hitDelayCounter.Stop();
            }
            if (angryCounter != null)
            {
                angryCounter.Stop();
            }

            GetComponent<AnimationHelper>().Play(AnimationReferences.TrollDead);
            GetComponent<AudioHelper>().Play(SoundReferences.TrollDeath);

            yield return new WaitForSeconds(2.15f);

            LeaveGame();
        }

        public void LeaveGame()
        {
            GetComponent<TrollController>().Listener.OnEnemyHurt(GetComponent<TrollController>());
            Destroy(gameObject);
        }

    }
}