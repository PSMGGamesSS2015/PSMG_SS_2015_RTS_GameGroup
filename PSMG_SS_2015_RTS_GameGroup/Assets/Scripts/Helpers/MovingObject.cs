using System;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public abstract class MovingObject : MonoBehaviour {

        public const float MovementSpeedWalking = 0.6f;
        public const float MovementSpeedRunning = 1.8f;

        public float MovementSpeed { get; protected set; }
        private float formerMovementSpeed;

        public Direction CurrentDirection;
        protected bool HasStartedMoving;
        protected bool IsStanding;

        public bool FacingRight { get; protected set; }

        public void Awake()
        {
            HasStartedMoving = false;
            FacingRight = true;
        }

        public abstract void Start();
        public abstract void FixedUpdate();

        public void Turn()
        {
            MovementSpeed *= -1;
            FacingRight = !FacingRight;
            this.Flip();
        }

        public void Move()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(MovementSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        public void MoveUpwards()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, MovementSpeed);
        }

        public void MoveUpwards(float movementSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, movementSpeed);
        }

        public void MoveDownwards()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -MovementSpeed);
        }

        public void MoveDownwards(float movementSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -movementSpeed);
        }

        public void StartMoving(bool facingRight, Direction direction) 
        {
            if (!facingRight) 
            {
                MovementSpeed = -MovementSpeedWalking;
            }
            else if (facingRight) 
            {
                MovementSpeed = MovementSpeedWalking;
            }
            this.FacingRight = facingRight;
            CurrentDirection = direction;
            HasStartedMoving = true;
        }

        public void Walk()
        {
            if (formerMovementSpeed < 0f)
            {
                MovementSpeed = -MovementSpeedWalking;
            }
            else if (formerMovementSpeed > 0f)
            {
                MovementSpeed = MovementSpeedWalking;
            }
            else if (IsStanding)
            {
                if (FacingRight)
                {
                    MovementSpeed = MovementSpeedWalking;
                }
                else
                {
                    MovementSpeed = -MovementSpeedWalking;
                }
                
            }
            IsStanding = false;
        }

        public void Run()
        {
            if (formerMovementSpeed < 0f || !FacingRight)
            {
                MovementSpeed = -MovementSpeedRunning;
            }
            else if (formerMovementSpeed > 0f || FacingRight)
            {
                MovementSpeed = MovementSpeedRunning;
            }
            IsStanding = false;
        }

        public void ChangeDirection(Direction direction)
        {
            CurrentDirection = direction;
        }

        public void Stand()
        {
            if (Math.Abs(MovementSpeed) > 0f)
            {
                formerMovementSpeed = MovementSpeed;
            }
            MovementSpeed = 0f;
            IsStanding = true;
        }

        public enum Direction
        {
            Horizontal,
            Vertical,
            Upwards,
            Downwards,
            Left,
            Right
        }

    }
}
