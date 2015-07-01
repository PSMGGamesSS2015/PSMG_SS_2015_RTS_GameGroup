using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpMovementService : MovingObject
    {
        public override void Start()
        {
            FacingRight = true;
            CurrentDirection = Direction.Horizontal;
            HasStartedMoving = true;
            IsStanding = true;
            Walk();
        }

        public override void FixedUpdate()
        {
            if (GetComponent<ImpTrainingService>().Type == ImpType.Coward || ((GetComponent<ImpTrainingService>().Type == ImpType.Spearman) && GetComponent<ImpSpearmanService>().IsInCommand())) return;
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