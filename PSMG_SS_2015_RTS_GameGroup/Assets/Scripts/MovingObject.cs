using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    private const float MOVEMENT_SPEED_WALKING = 0.6f;
    private const float MOVEMENT_SPEED_RUNNING = 1.8f;
    private float movementSpeed = 0.6f;
    private float formerMovementSpeed;
    private bool facingRight = true;
    private bool movingUpwards = false;
	
	private void FixedUpdate () {
        if (movingUpwards)
        {
            MoveUpwards();
        }
        else
        {
            Move();
        }   
	}

    private void Turn()
    {
        movementSpeed *= -1;
        facingRight = !facingRight;
        //Flip(gameObject);
        //this.Flip();
    }

    private void Move()
    {
        //rigidBody2D.velocity = new Vector2(movementSpeed, 0f);
    }

    private void MoveUpwards()
    {
        //rigidBody2D.velocity = new Vector2(0f, movementSpeed);
    }
}
