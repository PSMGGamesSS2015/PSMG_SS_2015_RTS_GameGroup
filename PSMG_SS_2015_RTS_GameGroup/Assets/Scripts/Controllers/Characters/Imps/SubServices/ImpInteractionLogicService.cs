using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpInteractionLogicService : MonoBehaviour
    {
        private ImpAnimationHelper impAnimationService;
        private ImpMovementService impMovementService;
        private ImpTrainingService impTrainingService;
        private ImpCollisionService impCollisionService;

        public void Awake()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            impMovementService = GetComponent<ImpMovementService>();
            impTrainingService = GetComponent<ImpTrainingService>();
            impCollisionService = GetComponent<ImpCollisionService>();
        }

        public void OnCollisionEnterWithImp(ImpController imp)
        {

            if (SpearmanAndCowardMeet(imp))
            {
                OnSpearmanMeetsCoward(imp);
            }

            else if (WalkingIntoImpThatIsBlockingTheWay(imp) )
            {
                OnWalkingIntoImpThatIsBlockingTheWay();
            }

            else if (WalkingIntoThrowingSchwarzenegger(imp))
            {
                OnWalkingIntoThrowingSchwarzenegger(imp);
            }

            else if (MeetingMovingImpAsSchwarzenegger(imp))
            {
                OnMeetMovingImpAsSchwarzenegger(imp);
            }
            else
            {
                OnDefaultInteraction(imp);
            }
        }

        private void OnWalkingIntoImpThatIsBlockingTheWay()
        {
            impMovementService.Turn();
        }

        private void OnWalkingIntoThrowingSchwarzenegger(ImpController imp)
        {
            // TODO Does not work
            if (imp.GetComponent<ImpSchwarzeneggerService>().CurrentProjectile != null &&
                GetComponent<ImpController>().GetInstanceID() !=
                imp.GetComponent<ImpSchwarzeneggerService>().CurrentProjectile.GetInstanceID())
            {
                impMovementService.Turn();
            }
        }

        private void OnDefaultInteraction(ImpController imp)
        {
            Physics2D.IgnoreCollision(impCollisionService.GetCollider(),
                imp.GetComponent<ImpCollisionService>().GetCollider(), true);
        }

        private void OnMeetMovingImpAsSchwarzenegger(ImpController imp)
        {
            if (GetComponent<ImpSchwarzeneggerService>().IsAtThrowingPosition ||
                (!GetComponent<ImpSchwarzeneggerService>().IsThrowing))
            {
                GetComponent<ImpSchwarzeneggerService>().ThrowImp(imp);
                Physics2D.IgnoreCollision(impCollisionService.GetCollider(),
                    imp.GetComponent<ImpCollisionService>().GetCollider(), true);
            }
        }

        private void OnSpearmanMeetsCoward(ImpController imp)
        {
            if (!SpearmanAndCowardHaveNoCommandPartner()) return;

            switch (impTrainingService.Type)
            {
                case ImpType.Spearman:
                    GetComponent<ImpSpearmanService>().FormCommand(imp);
                    break;
                case ImpType.Coward:
                    GetComponent<ImpCowardService>().FormCommand(imp);
                    break;
            }
        }


        private bool WalkingIntoThrowingSchwarzenegger(ImpController imp)
        {
            return (imp.GetComponent<ImpSchwarzeneggerService>() != null &&
                   imp.GetComponent<ImpSchwarzeneggerService>().IsThrowing) &&
                   HasAProfessionThatMoves();
        }

        private bool MeetingMovingImpAsSchwarzenegger(ImpController imp)
        {
            return (impTrainingService.Type == ImpType.Schwarzenegger) &&
                   ((imp.GetComponent<ImpTrainingService>().Type != ImpType.Schwarzenegger) ||
                    (imp.GetComponent<ImpTrainingService>().Type != ImpType.Coward));
        }

        private bool HasAProfessionThatMoves()
        {
            return (impTrainingService.Type == ImpType.Unemployed ||
                    impTrainingService.Type == ImpType.LadderCarrier ||
                    impTrainingService.Type == ImpType.Blaster ||
                    impTrainingService.Type == ImpType.Firebug ||
                    impTrainingService.Type == ImpType.Schwarzenegger);
        }

        private bool WalkingIntoImpThatIsBlockingTheWay(ImpController imp)
        {
            return ((imp.GetComponent<ImpTrainingService>().Type == ImpType.Coward) ||
                   (imp.GetComponent<ImpTrainingService>().Type == ImpType.LadderCarrier &&
                    imp.GetComponent<ImpLadderCarrierService>().IsPlacingLadder)) &&
                    HasAProfessionThatMoves();
        }

        private bool SpearmanAndCowardHaveNoCommandPartner()
        {
            return ((impTrainingService.Type == ImpType.Spearman) &&
                    GetComponent<ImpSpearmanService>().CommandPartner == null) ||
                   ((impTrainingService.Type == ImpType.Coward) &&
                    GetComponent<ImpCowardService>().CommandPartner == null);
        }

        private bool SpearmanAndCowardMeet(ImpController imp)
        {
            return ((impTrainingService.Type == ImpType.Spearman &&
                     imp.GetComponent<ImpTrainingService>().Type == ImpType.Coward) ||
                    (impTrainingService.Type == ImpType.Coward &&
                     imp.GetComponent<ImpTrainingService>().Type == ImpType.Spearman));
        }
    }
}