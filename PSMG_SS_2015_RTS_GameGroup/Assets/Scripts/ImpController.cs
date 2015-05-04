using UnityEngine;
using System.Collections;

public class ImpController : MonoBehaviour {

    Rigidbody2D rigidBody2D;
    BoxCollider2D boxCollider2D;
    public LayerMask blockingLayer;
    private float raycastLength = 0.3f;

    private float movementSpeed = 1f;
    bool facingRight = true;

    public delegate void Click(ImpController impController);
    public event Click OnImpSelected;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnMouseDown()
    {
        OnImpSelected(this);
    }

    void FixedUpdate()
    {

        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2(raycastLength, 0f);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        boxCollider2D.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        boxCollider2D.enabled = true;

        if (hit.transform != null)
        {
            movementSpeed *= -1;
            raycastLength *= -1;
        }

        rigidBody2D.velocity = new Vector2(movementSpeed, rigidBody2D.velocity.y);
    }


}
