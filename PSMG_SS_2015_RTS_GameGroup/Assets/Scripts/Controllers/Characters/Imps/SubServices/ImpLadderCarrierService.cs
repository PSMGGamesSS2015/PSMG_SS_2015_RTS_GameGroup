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
            if (verticalLadderSpotController.IsLadderPlaced) return;
            verticalLadderSpotController.IsLadderPlaced = true;
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

            float ladderPositionX;
            if (GetComponent<ImpMovementService>().FacingRight)
            {
                ladderPositionX = gameObject.transform.position.x + VerticalLadderPlacementOffsetHorizontal;
            }
            else
            {
                ladderPositionX = (gameObject.transform.position.x + VerticalLadderPlacementOffsetHorizontal * -1f);
            }
            var ladderPosition = new Vector3(ladderPositionX + VerticalLadderPlacementOffsetHorizontal,
                        gameObject.transform.position.y, gameObject.transform.position.z);

            switch (ladderLength)
            {
                case VerticalLadderSpotController.LadderLength.Long:
                    prefab = GetComponent<ImpController>().VerticalLadderLongPrefab;
                    break;
                case VerticalLadderSpotController.LadderLength.Medium:
                    prefab = GetComponent<ImpController>().VerticalLadderMediumPrefab;
                    
                    break;
            }

            yield return new WaitForSeconds(4f);

            var ladder = (GameObject)Instantiate(prefab, ladderPosition, Quaternion.identity);
            ladder.transform.parent = verticalLadderSpotController.GetComponent<Collider2D>().gameObject.transform.parent;

            impAnimationService.SwitchBackToStandardAnimation();
            IsPlacingLadder = false;
            impTrainingService.Untrain();
            impMovementService.Walk();
            GetComponent<ImpTrainingService>().IsTrainable = true;
        }

        public void SetupHorizontalLadder(LadderSpotController ladderSpotController)
        {
            if (ladderSpotController.IsLadderPlaced) return;
            ladderSpotController.IsLadderPlaced = true;
            StartCoroutine(SetupHorizontalLadderRoutine(ladderSpotController));
        }

        private IEnumerator SetupHorizontalLadderRoutine(LadderSpotController ladderSpotController)
        {
            var position = ladderSpotController.gameObject.transform.position;
            
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