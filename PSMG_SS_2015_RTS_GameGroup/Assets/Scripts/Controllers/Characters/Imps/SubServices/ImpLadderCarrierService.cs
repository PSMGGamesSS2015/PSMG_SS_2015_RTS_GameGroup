using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpLadderCarrierService : ImpProfessionService
    {

        private ImpTrainingService impTrainingService;
        private ImpMovementService impMovementService;
        private ImpAnimationHelper impAnimationService;
        private AudioHelper impAudioService;
        public bool IsPlacingLadder { get; private set; }

        public void Awake()
        {
            InitComponents();
            InitAttributes();
        }

        private void InitComponents()
        {
            impTrainingService = GetComponent<ImpTrainingService>();
            impMovementService = GetComponent<ImpMovementService>();
            impAnimationService = GetComponent<ImpAnimationHelper>();
            impAudioService = GetComponent<AudioHelper>();
        }

        private void InitAttributes()
        {
            IsPlacingLadder = false;
        }

        public GameObject SetupVerticalLadder()
        {

            var ladderPosition = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y + 1, gameObject.transform.position.z);

            var ladder = (GameObject)Instantiate(GetComponent<ImpController>().VerticalLadderPrefab, ladderPosition, Quaternion.identity);
            impTrainingService.Untrain();
            return ladder;
        }

        public void SetupHorizontalLadder(Vector3 position)
        {
            StartCoroutine(SetupHorizontalLadderRoutine(position));
        }

        private IEnumerator SetupHorizontalLadderRoutine(Vector3 position)
        {
            GetComponent<ImpTrainingService>().IsTrainable = false;
            impMovementService.Stand();
            IsPlacingLadder = true;

            impAnimationService.Play(AnimationReferences.ImpPlacingLadderHorizontally);
            impAudioService.Play(SoundReferences.ImpSetupLadder);

            yield return new WaitForSeconds(5.5f);

            impAnimationService.SwitchBackToStandardAnimation();
            IsPlacingLadder = false;
            impTrainingService.Untrain();
            impMovementService.Walk();
            Instantiate(GetComponent<ImpController>().HorizontalLadderPrefab, new Vector3(position.x+0.6f, position.y, 0), Quaternion.Euler(0, 0, -90));
            GetComponent<ImpTrainingService>().IsTrainable = true;
        }
    }
}