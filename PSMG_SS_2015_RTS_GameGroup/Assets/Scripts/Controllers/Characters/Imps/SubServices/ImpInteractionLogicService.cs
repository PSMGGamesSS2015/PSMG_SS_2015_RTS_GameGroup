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

        public void InteractWith(ImpController imp)
        {
            if (SpearmanAndCowardMeet(imp))
            {
                if (!SpearmanAndCowardHaveNoCommandPartner()) return;
                if (impTrainingService.Type == ImpType.Spearman)
                {
                    GetComponent<ImpSpearmanService>().FormCommand(imp);
                }

                if (impTrainingService.Type == ImpType.Coward)
                {
                    GetComponent<ImpCowardService>().FormCommand(imp);
                }
            }

            else if (MeetingCowardOrIsPlacingALadder(imp) && HasAProfessionThatMoves())
            {
                impMovementService.Turn();
            }

            else if (SchwarzeneggerMeetsMovingImp(imp))
            {
                if (GetComponent<ImpSchwarzeneggerService>().IsAtThrowingPosition)
                {
                    GetComponent<ImpSchwarzeneggerService>().ThrowImp(imp);
                }
            }
            else
            {
                Physics2D.IgnoreCollision(impCollisionService.GetCollider(),
                    imp.GetComponent<ImpCollisionService>().GetCollider(), true);
            }
        }

        private bool SchwarzeneggerMeetsMovingImp(ImpController imp)
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
                    impTrainingService.Type == ImpType.Botcher ||
                    impTrainingService.Type == ImpType.Schwarzenegger);
        }

        private bool MeetingCowardOrIsPlacingALadder(ImpController imp)
        {
            return (imp.GetComponent<ImpTrainingService>().Type == ImpType.Coward) ||
                    (imp.GetComponent<ImpTrainingService>().Type == ImpType.LadderCarrier &&
                     imp.GetComponent<ImpLadderCarrierService>().IsPlacingLadder);
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