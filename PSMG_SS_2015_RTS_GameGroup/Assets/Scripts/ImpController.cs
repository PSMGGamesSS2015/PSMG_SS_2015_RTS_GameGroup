using UnityEngine;
using System.Collections;

/// <summary>
/// The ImpController is a component attached to every instance of
/// an Imp prefab. It manages movement patterns and collision detection
/// of imps and listens for click events on the imps.
/// </summary>

public class ImpController : MonoBehaviour {
   
    public LayerMask blockingLayer;
    public LayerMask enemyLayer;
    
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private float raycastLength = 0.3f;
    private float movementSpeed = 1f;
    private ImpControllerListener listener;
    private Job job;

    public interface ImpControllerListener
    {
        void OnImpSelected(ImpController impController);
        void OnImpTrained(ImpController impController, Job job);
    }
    
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        job = Job.Unemployed;
    }

    private void OnMouseDown()
    {
        listener.OnImpSelected(this);
    }

    public void RegisterListener(ImpControllerListener listener)
    {
        this.listener = listener;
    }

    private void FixedUpdate()
    {
        if (IsPathBlockedByObstacle()) 
        {
            Turn();
        } 
        else if (IsPathBlockedByEnemy())
        {
            StopMoving();
        }
        else
        {
            Move();
        }
        
    }

    private bool IsPathBlockedByEnemy()
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(raycastLength, 0f);
        RaycastHit2D hit = Physics2D.Linecast(start, end, enemyLayer);
        return hit.transform != null;
    }

    private bool IsPathBlockedByObstacle()
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(raycastLength, 0f);
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        return hit.transform != null;
    }

    private void Turn()
    {
        movementSpeed *= -1;
        raycastLength *= -1;
    }

    private void Move()
    {
        rigidBody2D.velocity = new Vector2(movementSpeed, rigidBody2D.velocity.y);
    }

    private void StartMoving()
    {
        movementSpeed = 1.0f;
    }

    private void StopMoving()
    {
        movementSpeed = 0.0f;
    }

    public BoxCollider2D GetBoxCollider2D()
    {
        return boxCollider2D;
    }

    public bool HasJob()
    {
        return job != Job.Unemployed;
    }

    public Job GetJob()
    {
        return job;
    }

    public void Train(Job job)
    {
        this.job = job;
        listener.OnImpTrained(this, job);
        if (job == Job.Guardian)
        {
            StopMoving();
        }
    }

    public void Untrain()
    {
        job = Job.Unemployed;
        listener.OnImpTrained(this, job);
    }


    internal void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }
}
