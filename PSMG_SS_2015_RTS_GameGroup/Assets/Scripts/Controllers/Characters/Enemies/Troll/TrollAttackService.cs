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

        public Counter HitDelayCounter;
        public Counter AngryCounter;

        public void Awake()
        {
            triggerCollider2D = GetComponentInChildren<TriggerCollider2D>();
            impsInAttackRange = new List<ImpController>();
        }

        public void Start()
        {
            triggerCollider2D.RegisterListener(this);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != triggerCollider2D.GetInstanceID())
                return; // check if the triggered collider is attack range
            if (collider.gameObject.tag != TagReferences.Imp) return; // check if an imp enters the troll's attack range
            if (isSmashing) return; // check if the troll is striking with his weapon at this very moment

            impsInAttackRange.Add(collider.gameObject.GetComponent<ImpController>());

            if (GetComponent<TrollMoodService>().IsAngry)
                // if the troll is angry he attacks as soon as an imp enters his field of sight
            {
                StrikeWithMaul();
            }
            else
            {
                if (HitDelayCounter != null) return;
                HitDelayCounter = Counter.SetCounter(gameObject, 2.0f, StrikeWithMaul, true);
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (isSmashing) return;
            if (collider.gameObject.tag != TagReferences.Imp) return;

            if (!impsInAttackRange.Contains(collider.gameObject.GetComponent<ImpController>())) return;

            impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());

            if (impsInAttackRange.Count != 0) return;

            if (HitDelayCounter != null) // stop the counter when no more imps are in a trolls attack range
            {
                HitDelayCounter.Stop();
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            // not needed here
        }

        public void StrikeWithMaul()
        {
            StartCoroutine(SmashingRoutine());
        }

        private void SmashImpsBetweenCowardAndTroll(ImpController coward)
        {
            var distanceBetweenCowardAndTroll = Vector2.Distance(gameObject.transform.position,
                coward.gameObject.transform.position);

            var impsToBeHit = (from imp in impsInAttackRange
                let currentDistance = Vector2.Distance(gameObject.transform.position, imp.gameObject.transform.position)
                where currentDistance < distanceBetweenCowardAndTroll
                select imp).ToList();

            foreach (var imp in impsToBeHit)
            {
                impsInAttackRange.Remove(imp);
                imp.GetComponent<ImpPwnedService>().Pwn(ImpPwnedService.PwningType.Smashing); // actually hit the imps
            }

            if (impsInAttackRange.Count != 0) return;

            if (HitDelayCounter != null) HitDelayCounter.Stop();
        }

        private ImpController SearchForCoward()
        {
            return impsInAttackRange.FirstOrDefault(imp => imp.GetComponent<ImpTrainingService>().Type == ImpType.Coward);
        }

        private void SmashAllImpsInRange()
        {
            impsInAttackRange.ToList().ForEach(i => i.GetComponent<ImpPwnedService>().Pwn(ImpPwnedService.PwningType.Smashing));
            impsInAttackRange.Clear();
            if (HitDelayCounter != null) HitDelayCounter.Stop();
        }

        public IEnumerator SmashingRoutine()
        {
            GetComponent<TrollAudioService>().PlayAttackSound();
            GetComponent<AnimationHelper>().Play(AnimationReferences.TrollAttacking);

            yield return new WaitForSeconds(1f);

            if (!GetComponent<TrollController>().IsLeaving)
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

                if (!GetComponent<TrollController>().IsLeaving)
                {
                    GetComponent<AnimationHelper>().Play(AnimationReferences.TrollStanding);
                }
            }
            isSmashing = false;
        }

        public void StrikeAtOnce()
        {
            HitDelayCounter.Stop();
            StrikeWithMaul();
            AngryCounter = Counter.SetCounter(gameObject, 10f, GetComponent<TrollMoodService>().CalmDown, false);
        }

        public void StopCounters()
        {
            if (HitDelayCounter != null) HitDelayCounter.Stop();
            if (AngryCounter != null) AngryCounter.Stop();
        }
    }
}