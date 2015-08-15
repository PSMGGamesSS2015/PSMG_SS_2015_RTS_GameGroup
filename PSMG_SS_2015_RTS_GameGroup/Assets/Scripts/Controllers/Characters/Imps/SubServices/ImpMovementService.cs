using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpMovementService : MovingObject
    {
        private bool isBeingThrown;

        private const float ThrowingSpeedX = 7f;
        private const float ThrowingSpeedY = 7f;

        public override void Start()
        {
            isBeingThrown = false;
            IsJumping = false;
            FacingRight = true;
            CurrentDirection = Direction.Horizontal;
            HasStartedMoving = true;
            IsStanding = true;
            Walk();
        }

        public override void FixedUpdate()
        {
            if (isBeingThrown || IsFighting() || IsThrowing() || !HasStartedMoving || IsJumping) return;

            if (CurrentDirection == Direction.Vertical)
            {
                MoveUpwards();
            }
            else
            {
                Move();
            }
        }

        public bool IsJumping { get; set; }

        private bool IsThrowing()
        {
            return GetComponent<ImpSchwarzeneggerService>() != null &&
                   GetComponent<ImpSchwarzeneggerService>().IsAtThrowingPosition;
        }

        private bool IsFighting()
        {
            return GetComponent<ImpTrainingService>().Type == ImpType.Coward ||
                   ((GetComponent<ImpTrainingService>().Type == ImpType.Spearman) &&
                    GetComponent<ImpSpearmanService>().IsInCommand());
        }

        public void GetThrown()
        {
            isBeingThrown = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowingSpeedX, ThrowingSpeedY);
        }

        public void OnCollisionEnter2D(Collision2D collider)
        {
            if (collider.gameObject.tag == TagReferences.Imp) return;

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
                case ImpType.Firebug:
                    anim = AnimationReferences.ImpClimbingLadderFirebug;
                    break;
                default:
                    anim = AnimationReferences.ImpClimbingLadderUnemployed;
                    break;
            }

            GetComponent<ImpAnimationHelper>().Play(anim);
        }

        // TODO refactor
        public void Jump()
        {
            IsJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 2f);
        }

    }
}