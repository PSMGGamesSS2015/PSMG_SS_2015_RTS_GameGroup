using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies
{
    public class TrollController : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        #region variables and constants

        // general
        private Animator animator;
        private AudioHelper audioHelper;
        public EnemyType Type;
        private IEnemyControllerListener listener;
        public GameObject Counter;
        // troll
        private bool isAngry = false;
        private TriggerCollider2D triggerCollider2D;
        private List<ImpController> impsInAttackRange;
        private Counter hitDelay1;
        private bool isSmashing;
        private bool isLeaving;

        #endregion

        #region listener interface

        public interface IEnemyControllerListener
        {
            void OnEnemyHurt(TrollController trollController);
        }

        public void RegisterListener(IEnemyControllerListener listener)
        {
            this.listener = listener;
        }

        public void UnregisterListener()
        {
            listener = null;
        }

        #endregion

        #region initialization, update

        public void Awake()
        {
            InitComponents();
            InitTriggerColliders();
        }

        private void InitComponents()
        {
            animator = GetComponent<Animator>();
            audioHelper = GetComponent<AudioHelper>();
            isLeaving = false;
        }

        private void InitTriggerColliders()
        {
            triggerCollider2D = GetComponentInChildren<TriggerCollider2D>();
            triggerCollider2D.RegisterListener(this);
            impsInAttackRange = new List<ImpController>();
        }

        public void LeaveGame()
        {
            listener.OnEnemyHurt(this);
            Destroy(gameObject);
        }

        #endregion

        #region interface implementation

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (collider.gameObject.tag == TagReferences.Imp)
            {
                if (!isSmashing)
                {
                    impsInAttackRange.Add(collider.gameObject.GetComponent<ImpController>());
                    // TODO Work in conditions here: replace old counter with hitcounters
                    if (isAngry)
                    {
                        StrikeWithMaul();
                    }
                    else
                    {
                        if (hitDelay1 == null)
                        {
                            hitDelay1 = Instantiate(Counter).GetComponent<Counter>();
                            hitDelay1.Init(1f, StrikeWithMaul, true);
                        }
                    
                    }
                }
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (!isSmashing)
            {
                if (collider.gameObject.tag == TagReferences.Imp)
                {
                    if (impsInAttackRange.Contains(collider.gameObject.GetComponent<ImpController>()))
                    {
                        impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());
                        if (impsInAttackRange.Count == 0)
                        {
                            if (hitDelay1 != null)
                            {
                                hitDelay1.Stop();
                            }

                        }
                    }
                }
            }
        
        }

        #endregion

        #region troll battle-logic

        public void ReceiveHit()
        {
            isLeaving = true;
            StopCoroutine(SmashingRoutine());
            animator.Play(AnimationReferences.TrollStanding);
            StartCoroutine(LeavingRoutine());
        }

        private IEnumerator LeavingRoutine()
        {
            if (hitDelay1 != null)
            {
                hitDelay1.Stop();
            }
            animator.Play(AnimationReferences.TrollDead);
            audioHelper.Play(SoundReferences.TrollDeath);
            //this.StopAllCounters();

            yield return new WaitForSeconds(2.15f);
        
            LeaveGame();
        }

        private void StrikeWithMaul()
        {
            StartCoroutine(SmashingRoutine());
        }

        private void SmashImpsBetweenCowardAndTroll(ImpController coward)
        {
            float distanceBetweenCowardAndTroll = Vector2.Distance(gameObject.transform.position, coward.gameObject.transform.position);

            List<ImpController> impsToBeHit = new List<ImpController>();
            foreach (ImpController imp in impsInAttackRange)
            {
                float currentDistance = Vector2.Distance(gameObject.transform.position, imp.gameObject.transform.position);
                if (currentDistance < distanceBetweenCowardAndTroll)
                {
                    impsToBeHit.Add(imp); // Mark the imps to be hit
                }
            }

            foreach (ImpController imp in impsToBeHit)
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
            foreach (ImpController imp in impsInAttackRange)
            {
                if (imp.ImpTrainingService.Type == ImpType.Coward)
                {
                    return imp;
                }
            }
            return null;
        }

        private void SmashAllImpsInRange()
        {
            for (int i = impsInAttackRange.Count - 1; i >= 0; i--)
            {
                impsInAttackRange[i].LeaveGame();
            }

            impsInAttackRange.Clear();

            if (hitDelay1 != null)
            {
                hitDelay1.Stop();
            }
        }

        private IEnumerator SmashingRoutine() {

            audioHelper.Play(SoundReferences.TrollAttack2);

            yield return new WaitForSeconds(1f);
        
            if (!isLeaving)
            {
                animator.Play(AnimationReferences.TrollAttacking);

                ImpController coward = SearchForCoward(); // check if there is a coward within striking distance

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
                    animator.Play(AnimationReferences.TrollStanding);
                }
            
            }
            isSmashing = false;
        }

        #endregion

    }
}