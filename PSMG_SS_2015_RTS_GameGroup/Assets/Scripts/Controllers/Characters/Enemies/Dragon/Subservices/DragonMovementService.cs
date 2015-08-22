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
            MovementSpeed = MovementSpeedWalking;
            Walk();
        }

        public override void FixedUpdate()
        {
            switch (CurrentDirection)
            {
                case Direction.Upwards:
                    MoveUpwards(MovementSpeedWalking);
                    break;
                case Direction.Downwards:
                    MoveDownwards(MovementSpeedWalking * 0.5f);
                    break;
            }
        }

        public void ChangeDirection()
        {
            
            if (CurrentDirection == Direction.Upwards)
            {
                CurrentDirection = Direction.Downwards;
            }
            else
            {
                CurrentDirection = Direction.Upwards;
            }
        }

    }
}
