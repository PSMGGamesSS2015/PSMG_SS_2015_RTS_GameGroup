using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.ExtensionMethods;
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
        private bool isAngry = false;

        private Counter hitDelay1;
        private bool isLeaving;

        public void Awake()
        {
            triggerCollider2D = GetComponent<TriggerCollider2D>();
            triggerCollider2D.RegisterListener(this);

            impsInAttackRange = new List<ImpController>();
        }

        public void Start()
        {
            isLeaving = false;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (collider.gameObject.tag != TagReferences.Imp) return;
            if (isSmashing) return;
            impsInAttackRange.Add(collider.gameObject.GetComponent<ImpController>());
            // TODO Work in conditions here: replace old counter with hitcounters
            if (isAngry)
            {
                StrikeWithMaul();
            }
            else
            {
                if (hitDelay1 != null) return;
                hitDelay1 = Counter.SetCounter(gameObject, 4.0f, StrikeWithMaul, true);
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (isSmashing) return;
            if (collider.gameObject.tag != TagReferences.Imp) return;
            if (!impsInAttackRange.Contains(collider.gameObject.GetComponent<ImpController>())) return;
            impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());
            if (impsInAttackRange.Count != 0) return;
            if (hitDelay1 != null)
            {
                hitDelay1.Stop();
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
            if (hitDelay1 != null)
            {
                hitDelay1.Stop();
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

            if (hitDelay1 != null)
            {
                hitDelay1.Stop();
            }
        }

        public IEnumerator SmashingRoutine()
        {

            GetComponent<AudioHelper>().Play(SoundReferences.TrollAttack2);

            yield return new WaitForSeconds(1f);

            if (!isLeaving)
            {
                GetComponent<AnimationHelper>().Play(AnimationReferences.TrollAttacking);

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
            isLeaving = true;
            StopCoroutine(GetComponent<TrollAttackService>().SmashingRoutine());
            GetComponent<AnimationHelper>().Play(AnimationReferences.TrollStanding);
            StartCoroutine(LeavingRoutine());
        }

        private IEnumerator LeavingRoutine()
        {
            if (hitDelay1 != null)
            {
                hitDelay1.Stop();
            }
            GetComponent<AnimationHelper>().Play(AnimationReferences.TrollDead);
            GetComponent<AudioHelper>().Play(SoundReferences.TrollDeath);
            // TODO Check if this works
            this.StopAllCounters();

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