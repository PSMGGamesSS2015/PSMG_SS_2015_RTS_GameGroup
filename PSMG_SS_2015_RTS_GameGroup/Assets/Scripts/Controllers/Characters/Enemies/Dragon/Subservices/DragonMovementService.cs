using Assets.Scripts.Helpers;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonMovementService : MovingObject {
        
        private bool isMovingUpwards;

        private Counter movementCounter;

        public override void Start () {
            isMovingUpwards = true;
            movementCounter = Counter.SetCounter(this.gameObject, 4f, ChangeDirection, true);
            MovementSpeed = MovementSpeedWalking;
            Walk();
        }

        public override void FixedUpdate()
        {
            if (isMovingUpwards)
            {
                MoveUpwards();
            }
            else
            {
                MoveDownwards();
            }
        }

        private void ChangeDirection()
        {
            isMovingUpwards = !isMovingUpwards;
        }

        public void OnDestroy()
        {
            movementCounter.Stop();
        }

    }
}
