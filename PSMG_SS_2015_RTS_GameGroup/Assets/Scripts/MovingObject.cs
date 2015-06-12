using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    private const float MOVEMENT_SPEED_WALKING = 0.6f;
    private const float MOVEMENT_SPEED_RUNNING = 1.8f;
    
    private float movementSpeed;
    private float formerMovementSpeed;
    
    private bool facingRight = true;
    private Direction currentDirection;
    private bool hasStartedMoving;

    public bool FacingRight 
    {
        get
        {
            return facingRight;
        }
    }

    private void Awake()
    {
        hasStartedMoving = false;
    }

	private void FixedUpdate () {
        if (hasStartedMoving) 
        {
            if (currentDirection == Direction.Vertical)
            {
                MoveUpwards();
            }
            else
            {
                Move();
            }   
        }
	}

    private void Turn()
    {
        movementSpeed *= -1;
        facingRight = !facingRight;
        this.Flip();
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, 0f);
    }

    private void MoveUpwards()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, movementSpeed);
    }

    public void StartMoving(bool facingRight, Direction direction) 
    {
        if (!facingRight) 
        {
            movementSpeed = -MOVEMENT_SPEED_WALKING;
        }
        else if (facingRight) 
        {
            movementSpeed = MOVEMENT_SPEED_WALKING;
        }
        this.facingRight = facingRight;
        currentDirection = direction;
        hasStartedMoving = true;
    }

    public void Walk()
    {
        if (formerMovementSpeed < 0f)
        {
            movementSpeed = -MOVEMENT_SPEED_WALKING;
        }
        else if (formerMovementSpeed > 0f)
        {
            movementSpeed = MOVEMENT_SPEED_WALKING;
        }
    }

    public void Run()
    {
        if (formerMovementSpeed < 0f)
        {
            movementSpeed = -MOVEMENT_SPEED_RUNNING;
        }
        else if (formerMovementSpeed > 0f)
        {
            movementSpeed = MOVEMENT_SPEED_RUNNING;
        }
    }

    public void ChangeDirection(Direction direction)
    {
        currentDirection = direction;
    }

    public void Stand()
    {
        if (movementSpeed != 0f)
        {
            formerMovementSpeed = movementSpeed;
        }
        movementSpeed = 0f;
    }

    public enum Direction
    {
        Horizontal,
        Vertical
    }
}
