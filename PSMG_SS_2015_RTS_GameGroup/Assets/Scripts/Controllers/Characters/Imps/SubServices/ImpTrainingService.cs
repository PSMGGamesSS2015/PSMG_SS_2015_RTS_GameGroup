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
            impController = GetComponent<ImpController>();
            movementService = GetComponent<ImpMovementService>();
            impAnimationHelper = GetComponent<ImpAnimationHelper>();
            audioHelper = GetComponent<AudioHelper>();

            Type = ImpType.Unemployed;
            IsTrainable = true;
        }  

        public void Train(ImpType type)
        {
            StartCoroutine(TrainingRoutine(type));
        }

        private IEnumerator TrainingRoutine(ImpType type)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().freezeRotation = true;

            if (this.Type == ImpType.Schwarzenegger)
            {
                GetComponent<ImpSpriteManagerService>().SwapSprites();
            }

            if (this.Type != ImpType.Coward)
            {
                movementService.Stand();
            }
            impAnimationHelper.PlayImpTakingObjectAnimation();
            audioHelper.Play(SoundReferences.ImpSelect4);

            yield return new WaitForSeconds(1.0f);

            CheckIfCommandIsToBeDissolved();

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

        public bool SpearmanIsInCommand
        {
            get
            {
                return (Type == ImpType.Spearman) &&
                    GetComponent<ImpSpearmanService>().IsInCommand();
            }
        }

        public bool CowardIsInCommand
        {
            get
            {
                return Type == ImpType.Coward &&
                    GetComponent<ImpCowardService>().IsInCommand();
            }
        }

        public void CheckIfCommandIsToBeDissolved()
        {
            if (SpearmanIsInCommand)
            {
                GetComponent<ImpSpearmanService>().CommandPartner.GetComponent<ImpCowardService>().DissolveCommand();
                GetComponent<ImpSpearmanService>().DissolveCommand();
            }

            if (CowardIsInCommand)
            {
                GetComponent<ImpCowardService>().CommandPartner.GetComponent<ImpSpearmanService>().DissolveCommand();
                GetComponent<ImpCowardService>().DissolveCommand();
            }
        }

        private void TrainUnemployed()
        {
            movementService.Walk();
            impAnimationHelper.SwitchBackToStandardAnimation();
        }

        private void TrainCoward()
        {
            impAnimationHelper.PlayTrainingAnimation();
            audioHelper.Play(SoundReferences.ShieldWood1);
            currentProfessionService = gameObject.AddComponent<ImpCowardService>();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        private void TrainLadderCarrier()
        {
            impAnimationHelper.PlayTrainingAnimation();
            movementService.Walk();
            currentProfessionService = gameObject.AddComponent<ImpLadderCarrierService>();
        }

        private void TrainSpearman()
        {
            impAnimationHelper.PlayTrainingAnimation();
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
            impAnimationHelper.PlayWalkingAnimation();
        }

        private void TrainFirebug()
        {
            movementService.Walk();
            impAnimationHelper.PlayTrainingAnimation();
            currentProfessionService = gameObject.AddComponent<ImpFirebugService>();
        }

        private void RemoveCurrentProfessionService()
        {
            
            Destroy(currentProfessionService);
        }


        private void DisplayBlasterAnimation()
        {
            impAnimationHelper.PlayTrainingAnimation();
            movementService.Run();
        }

        public bool HasJob()
        {
            return Type != ImpType.Unemployed;
        }

        public void Untrain()
        {
            impController.Listeners.ForEach(l => l.OnUntrain(impController));
        }
    }
}