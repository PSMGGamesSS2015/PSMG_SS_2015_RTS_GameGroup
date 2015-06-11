using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour, TriggerCollider2D.TriggerCollider2DListener
{
    #region variables and constants

    // general
    private Animator animator;
    private AudioHelper audioHelper;
    public EnemyType type;
    private EnemyControllerListener listener;
    public GameObject counter;
    // troll
    private bool isAngry = false;
    private TriggerCollider2D triggerCollider2d;
    private List<ImpController> impsInAttackRange;
    private Counter hitDelay1;
    private bool isSmashing;
    private bool isLeaving;

    #endregion

    #region listener interface

    public interface EnemyControllerListener
    {
        void OnEnemyHurt(EnemyController enemyController);
    }

    public void RegisterListener(EnemyControllerListener listener)
    {
        this.listener = listener;
    }

    public void UnregisterListener()
    {
        listener = null;
    }

    #endregion

    #region initialization, update

    private void Awake()
    {
        InitComponents();
        InitTriggerColliders();
    }

    private void InitComponents()
    {
        animator = GetComponent<Animator>();
        audioHelper = GetComponent<AudioHelper>();
        isLeaving = true;
    }

    private void InitTriggerColliders()
    {
        triggerCollider2d = GetComponentInChildren<TriggerCollider2D>();
        triggerCollider2d.RegisterListener(this);
        impsInAttackRange = new List<ImpController>();
    }

    public void LeaveGame()
    {
        listener.OnEnemyHurt(this);
        Destroy(gameObject);
    }

    #endregion

    #region interface implementation

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
    {
        if (collider.gameObject.tag == TagReferences.IMP)
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
                    hitDelay1 = Instantiate(counter).GetComponent<Counter>();
                    hitDelay1.Init(1f, StrikeWithMaul, true);
                }
            }
        }
    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
    {
        if (!isSmashing)
        {
            if (collider.gameObject.tag == TagReferences.IMP)
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
        animator.Play(AnimationReferences.TROLL_STANDING);
        StartCoroutine(LeavingRoutine());
    }

    private IEnumerator LeavingRoutine()
    {
        animator.Play(AnimationReferences.TROLL_DEAD);
        audioHelper.Play(SoundReferences.TROLL_DEATH);
        this.StopAllCounters();

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
    }

    private ImpController SearchForCoward()
    {
        foreach (ImpController imp in impsInAttackRange)
        {
            if (imp.Type == ImpType.Coward)
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

        animator.Play(AnimationReferences.TROLL_ATTACKING);
        audioHelper.Play(SoundReferences.TROLL_ATTACK2);

        yield return new WaitForSeconds(1f);
        

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

        animator.Play(AnimationReferences.TROLL_STANDING);
        isSmashing = false;

    }

    #endregion

}