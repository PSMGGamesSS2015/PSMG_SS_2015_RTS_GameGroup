using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonMovementService : MovingObject {
        
        public override void Start ()
        {
            FacingRight = true;
            CurrentDirection = Direction.Upwards;
            HasStartedMoving = true;
            IsStanding = true;
            MovementSpeed = MovementSpeedRunning;
            Walk();
        }

        public override void FixedUpdate()
        {
            switch (CurrentDirection)
            {
                case Direction.Upwards:
                    MoveUpwards();
                    break;
                case Direction.Downwards:
                    MoveDownwards();
                    break;
            }
        }

        public void StayInPosition()
        {
            if (CurrentDirection == Direction.Upwards)
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
            }
            Stand();
        }

        public new void Run()
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            base.Run();
        }

        public void ChangeDirection()
        {
            
            if (CurrentDirection == Direction.Upwards)
            {
                CurrentDirection = Direction.Downwards;
                GetComponent<DragonSteamBreathingService>().ImpsInBreathingRange.Clear();
            }
            else
            {
                CurrentDirection = Direction.Upwards;
            }
        }

    }
}
