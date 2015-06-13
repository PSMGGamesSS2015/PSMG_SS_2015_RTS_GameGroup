using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
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
            if (this.Type != ImpType.Coward)
            {
                movementService.Stand();
            }
            impAnimationHelper.PlayImpTakingObjectAnimation();

            audioHelper.Play(SoundReferences.ImpSelect4);

            yield return new WaitForSeconds(1.0f);

            if ((this.Type == ImpType.Spearman) && GetComponent<ImpSpearmanService>().IsInCommand())
            {
                GetComponent<ImpSpearmanService>().CommandPartner.GetComponent<ImpCowardService>().DissolveCommand();
                GetComponent<ImpSpearmanService>().DissolveCommand();
            }

            if ((this.Type == ImpType.Coward) && GetComponent<ImpCowardService>().IsInCommand())
            {
                GetComponent<ImpCowardService>().CommandPartner.GetComponent<ImpSpearmanService>().DissolveCommand();
                GetComponent<ImpCowardService>().DissolveCommand();
            }

            this.Type = type;

            if (type == ImpType.Blaster) TrainBlaster();

            if (type == ImpType.Spearman) TrainSpearman();

            if (type == ImpType.LadderCarrier) TrainLadderCarrier();

            if (type == ImpType.Unemployed) TrainUnemployed();

            if (type == ImpType.Coward) TrainCoward();

        }

        private void TrainUnemployed()
        {
            movementService.Walk();
            impAnimationHelper.SwitchBackToStandardAnimation();
            RemoveCurrentProfessionService();
        }

        private void TrainCoward()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.Coward);
            audioHelper.Play(SoundReferences.ShieldWood1);
            RemoveCurrentProfessionService();
            currentProfessionService = gameObject.AddComponent<ImpCowardService>();
        }

        private void TrainLadderCarrier()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.LadderCarrier);
            movementService.Walk();
            RemoveCurrentProfessionService();
            currentProfessionService = gameObject.AddComponent<ImpLadderCarrierService>();
        }

        private void TrainSpearman()
        {
            impAnimationHelper.PlayTrainingAnimation(ImpType.Spearman);
            movementService.Walk();
            RemoveCurrentProfessionService();
            currentProfessionService = gameObject.AddComponent<ImpSpearmanService>();
        }

        private void RemoveCurrentProfessionService()
        {
            Destroy(currentProfessionService);
        }

        private void TrainBlaster()
        {
            IsTrainable = false;
            SetupBombCounter();
            DisplayBlasterAnimation();
            RemoveCurrentProfessionService();
            currentProfessionService = gameObject.AddComponent<ImpBlasterService>();
        }

        private void SetupBombCounter()
        {
            if (impController.bombCounter != null) Destroy(impController.bombCounter.gameObject);
            impController.bombCounter = Counter.SetCounter(this.gameObject, 3f, impController.DetonateBomb, false);
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