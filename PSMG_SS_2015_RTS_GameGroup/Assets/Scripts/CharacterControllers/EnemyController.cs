using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour, TriggerCollider2D.TriggerCollider2DListener
{

    public EnemyType type;
    private TriggerCollider2D triggerCollider2d;
    private float hitDelay = 0f;
    private float angryCounter = 0f;
    private List<ImpController> impsInAttackRange;
    private bool isAngry = true;
    private EnemyControllerListener listener;

    public interface EnemyControllerListener
    {
        void OnEnemyHurt(EnemyController enemyController);
    }

    private void Awake()
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

    public void RegisterListener(EnemyControllerListener listener)
    {
        this.listener = listener;
    }

    public void UnregisterListener()
    {
        listener = null;
    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerEnter2D(Collider2D collider)
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

    private void StartCounter()
    {
        hitDelay = 0f;
    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Imp")
        {
            Debug.Log("An imp has left the sight of the troll.");
            impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());
            hitDelay = 0f;
        }
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

    private void LeaveGame()
    {
        listener.OnEnemyHurt(this);
        Destroy(gameObject);
    }

    private void StrikeWithMaul()
    {
        foreach (ImpController imp in impsInAttackRange)
        {
            imp.LeaveGame();
        }
        impsInAttackRange.Clear();
    }

}