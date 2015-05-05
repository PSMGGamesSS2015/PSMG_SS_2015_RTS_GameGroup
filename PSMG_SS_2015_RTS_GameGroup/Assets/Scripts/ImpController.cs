using UnityEngine;
using System.Collections;

public class ImpController : MonoBehaviour {

   public LayerMask blockingLayer;
    
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private float raycastLength = 0.3f;
    private float movementSpeed = 1f;
    private ImpControllerListener listener;

    public interface ImpControllerListener
    {
        void OnImpSelected(ImpController impController);
    }

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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
        if (IsPathBlocked()) 
        {
            Turn();
        }
        Move();
    }

    private bool IsPathBlocked()
    {
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(raycastLength, 0f);
        boxCollider2D.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider2D.enabled = true;
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

}
