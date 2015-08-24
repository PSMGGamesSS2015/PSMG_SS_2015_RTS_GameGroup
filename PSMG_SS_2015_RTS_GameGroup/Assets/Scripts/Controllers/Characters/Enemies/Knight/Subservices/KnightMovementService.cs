using Assets.Scripts.Helpers;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightMovementService : MovingObject
    {
        public override void Start()
        {
            FacingRight = false;
            CurrentDirection = Direction.Horizontal;
            HasStartedMoving = true;
            IsStanding = true;
        }

        public override void FixedUpdate()
        {
            Move();
        }
    }
}