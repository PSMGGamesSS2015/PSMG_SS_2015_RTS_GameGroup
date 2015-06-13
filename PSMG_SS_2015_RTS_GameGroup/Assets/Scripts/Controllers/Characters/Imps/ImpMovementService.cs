using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpMovementService : MovingObject
    {
        public override void Start()
        {
            facingRight = true;
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
            MoveUpwards();
        }

        private void PlayClimbingAnimation()
        {
            string anim;
            if (GetComponent<ImpTrainingService>().Type == ImpType.Spearman)
            {
                anim = AnimationReferences.ImpClimbingLadderSpearman;
            }
            else
            {
                anim = AnimationReferences.ImpClimbingLadderUnemployed;
            }
            GetComponent<ImpAnimationHelper>().Play(anim);
        }
    }
}