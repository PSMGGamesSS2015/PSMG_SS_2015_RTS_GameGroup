using System;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class MovingObject : MonoBehaviour {

        public const float MovementSpeedWalking = 0.6f;
        public const float MovementSpeedRunning = 1.8f;
    
        public float movementSpeed;
        public float formerMovementSpeed;
    
        public bool facingRight = true;
        protected Direction CurrentDirection;
        protected bool HasStartedMoving;
        protected bool IsStanding;

        public bool FacingRight 
        {
            get
            {
                return facingRight;
            }
        }

        public void Awake()
        {
            HasStartedMoving = false;
        }

        public abstract void Start();
        public abstract void FixedUpdate();


        public void Turn()
        {
            movementSpeed *= -1;
            facingRight = !facingRight;
            this.Flip();
        }

        public void Move()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, 0f);
        }

        public void MoveUpwards()
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
            CurrentDirection = direction;
            HasStartedMoving = true;
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
            else if (IsStanding)
            {
                if (facingRight)
                {
                    movementSpeed = MovementSpeedWalking;
                }
                else
                {
                    movementSpeed = -MovementSpeedWalking;
                }
                
            }
            IsStanding = false;
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
            IsStanding = false;
        }

        public void ChangeDirection(Direction direction)
        {
            CurrentDirection = direction;
        }

        public void Stand()
        {
            if (Math.Abs(movementSpeed) > 0f)
            {
                formerMovementSpeed = movementSpeed;
            }
            movementSpeed = 0f;
            IsStanding = true;
        }

        public enum Direction
        {
            Horizontal,
            Vertical
        }
    }
}
