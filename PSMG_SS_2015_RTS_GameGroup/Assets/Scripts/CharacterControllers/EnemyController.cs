using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour, TriggerCollider2D.TriggerCollider2DListener
{
    #region variables and constants

    // general
    public EnemyType type;
    private EnemyControllerListener listener;
    // troll
    private bool isAngry = false;
    private float hitDelay = 0f;
    private float angryCounter = 0f;
    private TriggerCollider2D triggerCollider2d;
    private List<ImpController> impsInAttackRange;

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
        InitTriggerColliders();
    }

    private void InitTriggerColliders()
    {
        triggerCollider2d = GetComponentInChildren<TriggerCollider2D>();
        triggerCollider2d.RegisterListener(this);
        impsInAttackRange = new List<ImpController>();
    }

    private void Update()
    {
        if (impsInAttackRange.Count > 0)
        {
            hitDelay += Time.deltaTime;
            if (hitDelay >= 1.0f)
            {
                StrikeWithMaul();
            }
        }
        if (isAngry)
        {
            angryCounter += Time.deltaTime;
            if (angryCounter >= 4.0f)
            {
                isAngry = false;
                angryCounter = 0f;
            }
        }
        
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
        if (collider.gameObject.tag == "Imp")
        {
            Debug.Log("An imp has entered the sight of the troll.");
            impsInAttackRange.Add(collider.gameObject.GetComponent<ImpController>());
            if (isAngry)
            {
                StrikeWithMaul();
            }
            else
            {
                StartCounter();
            }
        }
    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
    {
        if (collider.gameObject.tag == "Imp")
        {
            Debug.Log("An imp has left the sight of the troll.");
            impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());
            hitDelay = 0f;
        }
    }

    #endregion

    #region troll battle-logic

    private void StartCounter()
    {
        hitDelay = 0f;
    }

    public void ReceiveHit()
    {
        if (isAngry)
        {
            LeaveGame();
        }
        else
        {
            isAngry = true;
            StartAngryCounter();
            StrikeWithMaul();
        }
    }

    private void StartAngryCounter()
    {
        angryCounter = 0f;
    }

    private void StopAngryCounter()
    {
        angryCounter = 0f;
    }



    private void StrikeWithMaul()
    {
        ImpController coward = SearchForCoward(); // check if there is a coward within striking distance

        if (coward != null)
        {
            SmashImpsBetweenCowardAndTroll(coward);
        }
        else
        {
            SmashAllImpsInRange();
        }


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
        impsToBeHit.Clear();
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
        foreach (ImpController imp in impsInAttackRange)
        {
            imp.LeaveGame();
        }
        impsInAttackRange.Clear();
    }

    #endregion

}