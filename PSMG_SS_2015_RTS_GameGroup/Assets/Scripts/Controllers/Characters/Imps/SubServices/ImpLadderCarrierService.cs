using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpLadderCarrierService : ImpProfessionService
    {
        private const float VerticalLadderPlacementOffsetVertical = 1f;
        private const float VerticalLadderPlacementOffsetHorizontal = 0.5f;

        private ImpTrainingService impTrainingService;
        private ImpMovementService impMovementService;
        private ImpAnimationHelper impAnimationService;
        private AudioHelper impAudioService;
        public bool IsPlacingLadder { get; private set; }
        public GameObject CurrentLadder { get; private set; }


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

        public void SetupVerticalLadder(VerticalLadderSpotController verticalLadderSpotController)
        {
            StartCoroutine(SetupVerticalLadderRoutine(verticalLadderSpotController));
        }

        private IEnumerator SetupVerticalLadderRoutine(VerticalLadderSpotController verticalLadderSpotController)
        {
            GetComponent<ImpTrainingService>().IsTrainable = false;
            impMovementService.Stand();
            IsPlacingLadder = true;

            var ladderLength = verticalLadderSpotController.LengthOfLadder;

            impAnimationService.Play(AnimationReferences.ImpPlacingLadderVertically);
            impAudioService.Play(SoundReferences.ImpSetupLadder);

            GameObject prefab = null;
            var ladderPosition = new Vector3();

            switch (ladderLength)
            {
                case VerticalLadderSpotController.LadderLength.Long:
                    prefab = GetComponent<ImpController>().VerticalLadderLongPrefab;
                    ladderPosition = new Vector3(gameObject.transform.position.x + VerticalLadderPlacementOffsetHorizontal,
                        gameObject.transform.position.y + VerticalLadderPlacementOffsetVertical, gameObject.transform.position.z);
                    break;
                case VerticalLadderSpotController.LadderLength.Medium:
                    prefab = GetComponent<ImpController>().VerticalLadderMediumPrefab;
                    ladderPosition = new Vector3(gameObject.transform.position.x + VerticalLadderPlacementOffsetHorizontal,
                        gameObject.transform.position.y, gameObject.transform.position.z);
                    break;
            }

            yield return new WaitForSeconds(4f);

            CurrentLadder = (GameObject) Instantiate(prefab, ladderPosition, Quaternion.identity);
            // TODO refactor
            var ladder = GetComponent<ImpLadderCarrierService>().CurrentLadder;
            
            ladder.transform.parent = verticalLadderSpotController.GetComponent<Collider2D>().gameObject.transform.parent;

            verticalLadderSpotController.PlaceLadder();

            impAnimationService.SwitchBackToStandardAnimation();
            IsPlacingLadder = false;
            impTrainingService.Untrain();
            impMovementService.Walk();
            GetComponent<ImpTrainingService>().IsTrainable = true;
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
            Instantiate(GetComponent<ImpController>().HorizontalLadderPrefab,
                new Vector3(position.x + 0.6f, position.y, 0), Quaternion.Euler(0, 0, -90));
            GetComponent<ImpTrainingService>().IsTrainable = true;
        }
    }
}