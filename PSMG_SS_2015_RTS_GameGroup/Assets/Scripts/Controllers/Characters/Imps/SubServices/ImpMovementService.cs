using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpMovementService : MovingObject
    {
        private bool isBeingThrown;

        private const float ThrowingSpeedX = 5f;
        private const float ThrowingSpeedY = 5f;

        public override void Start()
        {
            isBeingThrown = false;

            FacingRight = true;
            CurrentDirection = Direction.Horizontal;
            HasStartedMoving = true;
            IsStanding = true;
            Walk();
        }

        public override void FixedUpdate()
        {
            if (isBeingThrown) return;
            // TODO refactor

            if (GetComponent<ImpTrainingService>().Type == ImpType.Coward ||
                ((GetComponent<ImpTrainingService>().Type == ImpType.Spearman) &&
                 GetComponent<ImpSpearmanService>().IsInCommand())) return;
            if (GetComponent<ImpSchwarzeneggerService>() != null &&
                GetComponent<ImpSchwarzeneggerService>().IsAtThrowingPosition) return;

            if (!HasStartedMoving) return;
            if (CurrentDirection == Direction.Vertical)
            {
                MoveUpwards();
            }
            else
            {
                Move();
            }
        }

        public void GetThrown()
        {
            // TODO consider facing direction

            isBeingThrown = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowingSpeedX, ThrowingSpeedY);
        }

        public void OnCollisionEnter2D(Collision2D collider)
        {
            if (isBeingThrown)
            {
                isBeingThrown = false;
            }

        }

        public void ClimbLadder()
        {
            PlayClimbingAnimation();
            CurrentDirection = Direction.Vertical;
        }

        private void PlayClimbingAnimation()
        {
            string anim;
            switch (GetComponent<ImpTrainingService>().Type)
            {
                case ImpType.Spearman:
                    anim = AnimationReferences.ImpClimbingLadderSpearman;
                    break;
                case ImpType.Unemployed:
                    anim = AnimationReferences.ImpClimbingLadderUnemployed;
                    break;
                default:
                    anim = AnimationReferences.ImpClimbingLadderUnemployed;
                    break;
            }

            GetComponent<ImpAnimationHelper>().Play(anim);
        }
    }
}