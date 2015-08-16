using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpMovementService : MovingObject
    {
        private bool isBeingThrown;
        public bool IsClimbing { get; set; }

        private const float ThrowingSpeedX = 7f;
        private const float ThrowingSpeedY = 7f;

        public override void Start()
        {
            isBeingThrown = false;
            IsClimbing = false;
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

        // TODO refactor

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

        public void Jump()
        {
            IsJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 2f);
            // TODO Play jumping animation
        }

        public void ClimbALittleHigher()
        {
            Counter.SetCounter(gameObject, 6f, StopClimbing, false);
        }

        private void StopClimbing()
        {
            StartCoroutine(StopClimbingRoutine());
        }

        private IEnumerator StopClimbingRoutine()
        {
            IsClimbing = false;
            GetComponent<ImpCollisionService>().StopIgnoringCollisions();
            GetComponent<ImpSpriteManagerService>().MoveToDefaultSortingLayer();

            Jump();
            
            yield return new WaitForSeconds(2f);

            CurrentDirection = Direction.Horizontal;
            GetComponent<AudioHelper>().Play(SoundReferences.ImpGoing);

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpTrainingService>().IsTrainable = true;

            IsJumping = false;
        }

    }
}