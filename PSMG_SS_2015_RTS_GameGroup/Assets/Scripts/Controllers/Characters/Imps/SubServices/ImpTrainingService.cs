using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpTrainingService : MonoBehaviour
    {
        private ImpController impController;
        private ImpMovementService movementService;
        private ImpAnimationHelper impAnimationHelper;
        private ImpProfessionService currentProfessionService;
        private AudioHelper audioHelper;

        public bool IsTrainable { get; set; }
        public ImpType Type { get; set; }

        public void Awake()
        {
            InitComponents();
            InitAttributes();
        }

        private void InitAttributes()
        {
            Type = ImpType.Unemployed;
            IsTrainable = true;
        }

        private void InitComponents()
        {
            impController = GetComponent<ImpController>();
            movementService = GetComponent<ImpMovementService>();
            impAnimationHelper = GetComponent<ImpAnimationHelper>();
            audioHelper = GetComponent<AudioHelper>();
        }

        public void Train(ImpType type)
        {
            StartCoroutine(TrainingRoutine(type));
        }

        private IEnumerator TrainingRoutine(ImpType type)
        {

            if (this.Type == ImpType.Schwarzenegger)
            {
                GetComponent<ImpAnimationHelper>().SwapSprites();
            }

            if (this.Type != ImpType.Coward)
            {
                movementService.Stand();
            }
            impAnimationHelper.PlayImpTakingObjectAnimation();
            audioHelper.Play(SoundReferences.ImpSelect4);

            yield return new WaitForSeconds(1.0f);

            if (SpearmanIsInCommand())
            {
                GetComponent<ImpSpearmanService>().CommandPartner.GetComponent<ImpCowardService>().DissolveCommand();
                GetComponent<ImpSpearmanService>().DissolveCommand();
            }

            if (CowardIsInCommand())
            {
                GetComponent<ImpCowardService>().CommandPartner.GetComponent<ImpSpearmanService>().DissolveCommand();
                GetComponent<ImpCowardService>().DissolveCommand();
            }

            this.Type = type;

            RemoveCurrentProfessionService();
            switch (type)
            {
                case ImpType.Spearman:
                    TrainSpearman();
                    break;
                case ImpType.Coward:
                    TrainCoward();
                    break;
                case ImpType.LadderCarrier:
                    TrainLadderCarrier();
                    break;
                case ImpType.Blaster:
                    TrainBlaster();
                    break;
                case ImpType.Firebug:
                    TrainFirebug();
                    break;
                case ImpType.Schwarzenegger:
                    TrainSchwarzenegger();
                    break;
                case ImpType.Unemployed:
                    TrainUnemployed();
                    break;
            }
        }


        private bool CowardIsInCommand()
        {
            return (this.Type == ImpType.Coward) && GetComponent<ImpCowardService>().IsInCommand();
        }

        private bool SpearmanIsInCommand()
        {
            return (this.Type == ImpType.Spearman) && GetComponent<ImpSpearmanService>().IsInCommand();
        }

        private void TrainUnemployed()
        {
            movementService.Walk();
            impAnimationHelper.SwitchBackToStandardAnimation();
        }

        private void TrainCoward()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.Coward);
            audioHelper.Play(SoundReferences.ShieldWood1);
            currentProfessionService = gameObject.AddComponent<ImpCowardService>();
        }

        private void TrainLadderCarrier()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.LadderCarrier);
            movementService.Walk();
            currentProfessionService = gameObject.AddComponent<ImpLadderCarrierService>();
        }

        private void TrainSpearman()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.Spearman);
            movementService.Walk();
            currentProfessionService = gameObject.AddComponent<ImpSpearmanService>();
        }

        private void TrainBlaster()
        {
            IsTrainable = false;
            DisplayBlasterAnimation();
            currentProfessionService = gameObject.AddComponent<ImpBlasterService>();
        }

        private void TrainSchwarzenegger()
        {
            movementService.Walk();
            currentProfessionService = gameObject.AddComponent<ImpSchwarzeneggerService>();
            // TODO
            impAnimationHelper.PlayWalkingAnimation(ImpType.Unemployed);
        }

        private void TrainFirebug()
        {
            movementService.Walk();
            impAnimationHelper.PlayTrainingAnimation(ImpType.Firebug);
            currentProfessionService = gameObject.AddComponent<ImpFirebugService>();
            // TODO
        }

        private void RemoveCurrentProfessionService()
        {
            
            Destroy(currentProfessionService);
        }


        private void DisplayBlasterAnimation()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.Blaster);
            movementService.Run();
        }

        public bool HasJob()
        {
            return Type != ImpType.Unemployed;
        }

        public void Untrain()
        {
            foreach (var listener in impController.Listeners)
            {
                listener.OnUntrain(impController);
            }
        }
    }
}