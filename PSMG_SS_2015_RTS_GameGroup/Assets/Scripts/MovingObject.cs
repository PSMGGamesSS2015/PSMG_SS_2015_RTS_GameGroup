using System;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovingObject : MonoBehaviour {

        private const float MovementSpeedWalking = 0.6f;
        private const float MovementSpeedRunning = 1.8f;
    
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

        public void Awake()
        {
            hasStartedMoving = false;
        }

        public void FixedUpdate () {
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

        public void Turn()
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
                movementSpeed = -MovementSpeedWalking;
            }
            else if (facingRight) 
            {
                movementSpeed = MovementSpeedWalking;
            }
            this.facingRight = facingRight;
            currentDirection = direction;
            hasStartedMoving = true;
        }

        public void Walk()
        {
            if (formerMovementSpeed < 0f)
            {
                movementSpeed = -MovementSpeedWalking;
            }
            else if (formerMovementSpeed > 0f)
            {
                movementSpeed = MovementSpeedWalking;
            }
        }

        public void Run()
        {
            if (formerMovementSpeed < 0f)
            {
                movementSpeed = -MovementSpeedRunning;
            }
            else if (formerMovementSpeed > 0f)
            {
                movementSpeed = MovementSpeedRunning;
            }
        }

        public void ChangeDirection(Direction direction)
        {
            currentDirection = direction;
        }

        public void Stand()
        {
            if (Math.Abs(movementSpeed) > 0f)
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
}
