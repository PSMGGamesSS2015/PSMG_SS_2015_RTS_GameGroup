using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour, TriggerCollider2D.TriggerCollider2DListener
{

    public EnemyType type;
    private TriggerCollider2D triggerCollider2d;
    private float hitDelay = 0f;
    private List<ImpController> impsInAttackRange;

    private void Awake()
    {
        triggerCollider2d = GetComponentInChildren<TriggerCollider2D>();
        triggerCollider2d.RegisterListener(this);
        impsInAttackRange = new List<ImpController>();
    }

    private void InteractWith(ImpController imp)
    {
        // Smash imp
        
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

        
    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Imp")
        {
            Debug.Log("An imp has entered the sight of the troll.");
            impsInAttackRange.Add(collider.gameObject.GetComponent<ImpController>());
            StartCounter();
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
            impsInAttackRange.Remove(collider.gameObject.GetComponent<ImpController>());
            StopCounter();
            Debug.Log("An imp has left the sight of the troll.");
        }
    }

    private void StopCounter()
    {
        hitDelay = 0f;
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