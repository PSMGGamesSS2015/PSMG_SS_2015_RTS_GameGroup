using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpCollisionService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private CircleCollider2D circleCollider2D;
        private TriggerCollider2D impCollisionCheck;
        private TriggerCollider2D impClickCheck;
        private ImpAnimationHelper impAnimationService;
        private AudioHelper impAudioService;
        private ImpMovementService impMovementService;
        private ImpTrainingService impTrainingService;

        public void Awake()
        {
            InitComponents();
            InitTriggerColliders();
        }

        private void InitComponents()
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
            impAnimationService = GetComponent<ImpAnimationHelper>();
            impAudioService = GetComponent<AudioHelper>();
            impMovementService = GetComponent<ImpMovementService>();
            impTrainingService = GetComponent<ImpTrainingService>();
        }

        private void InitTriggerColliders()
        {
            var triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

            // TODO Refactor using linq

            foreach (var c in triggerColliders)
            {
                switch (c.tag)
                {
                    case TagReferences.ImpCollisionCheck:
                        impCollisionCheck = c;
                        break;
                    case TagReferences.ImpClickCheck:
                        impClickCheck = c;
                        break;
                }
            }
            impCollisionCheck.RegisterListener(this);
            impClickCheck.RegisterListener(this);
        }

        public CircleCollider2D GetCollider()
        {
            return circleCollider2D;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var tag = collision.gameObject.tag;

            switch (tag)
            {
                case TagReferences.EnemyTroll:
                    impMovementService.Turn();
                    break;
                case TagReferences.Imp:
                    var imp = collision.gameObject.GetComponent<ImpController>();
                    GetComponent<ImpInteractionLogicService>().InteractWith(imp);
                    break;
                case TagReferences.Obstacle:
                    impMovementService.Turn();
                    break;
                case TagReferences.FragileRock:
                    impMovementService.Turn();
                    break;
                case TagReferences.Impassable:
                    impMovementService.Turn();
                    break;
            }
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            var imp = collision.gameObject.GetComponent<ImpController>();
            if (imp == null) return;
            if (imp.GetComponent<ImpTrainingService>().Type != ImpType.Coward) return;
            if (impTrainingService.Type != ImpType.Spearman)
            {
                impMovementService.Turn();
            }
        }

        // TODO Refactor this method

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != impCollisionCheck.GetInstanceID()) return;

            var tag = collider.gameObject.tag;

            switch (tag)
            {
                case TagReferences.LadderSpotVertical:
                    
                    if (impTrainingService.Type == ImpType.LadderCarrier)
                    {
                        
                        var ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
                        if (!ladderSpotController.IsLadderPlaced)
                        {
                            var ladder = GetComponent<ImpLadderCarrierService>()
                                .SetupVerticalLadder();

                            // TODO refactor
                            ladder.transform.parent = collider.gameObject.transform.parent;

                            ladderSpotController.PlaceLadder();
                        }
                    }
                    break;
                case TagReferences.LadderSpotHorizontal:

                    if (impTrainingService.Type == ImpType.LadderCarrier)
                    {
                        var ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
                        if (!ladderSpotController.IsLadderPlaced)
                        {
                            GetComponent<ImpLadderCarrierService>()
                                .SetupHorizontalLadder(collider.gameObject.transform.position);
                            ladderSpotController.PlaceLadder();
                        }
                    }
                    break;
                case TagReferences.LadderBottom:
                    if (GetComponent<ImpMovementService>().FacingRight)
                    {
                        GetComponent<ImpMovementService>().ClimbLadder();
                        impTrainingService.IsTrainable = false;
                    }
                    break;
                case TagReferences.LadderTop:
                    // TODO change all this
                    //impMovementService.CurrentDirection = MovingObject.Direction.Horizontal;
                    //impAudioService.Play(SoundReferences.ImpGoing);
                    //impAnimationService.PlayWalkingAnimation(impTrainingService.Type);
                    //impTrainingService.IsTrainable = true;
                    break;
                case TagReferences.Gaslight:
                    if (GetComponent<ImpFirebugService>() == null) return;
                    GetComponent<ImpFirebugService>().LightGaslight(collider.gameObject.GetComponent<GaslightController>());
                    break;
                case TagReferences.BurningObject:
                    if (GetComponent<ImpFirebugService>() == null) return;
                    GetComponent<ImpFirebugService>().SetOnFire(collider.gameObject, 5);
                    break;
                case TagReferences.SchwarzeneggerSpot:
                    if (GetComponent<ImpSchwarzeneggerService>() == null) return;
                    if (collider.GetComponent<SchwarzeneggerSpotController>() == null) return;
                    GetComponent<ImpSchwarzeneggerService>().IsAtThrowingPosition = true;
                    break;
            }
        }

        // TODO Check

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != impCollisionCheck.GetInstanceID()) return;

            var tag = collider.gameObject.tag;

            switch (tag)
            {
                case TagReferences.Imp:
                    var imp = collider.gameObject.GetComponent<ImpController>();
                    Physics2D.IgnoreCollision(GetCollider(), imp.GetComponent<ImpCollisionService>().GetCollider(), false);
                    break;
                case TagReferences.LadderTop:
                    Debug.Log("Has reached top of ladder");
                    // TODO climb a little higher
                    Counter.SetCounter(gameObject, 2f, ClimbALittleHigher, false);
                    // TODO play reaching end of ladder animation
                    impAnimationService.Play(AnimationReferences.ImpClimbingLadderEnd);
                    // TODO then start moving again
                    break;
            }

        }

        private void ClimbALittleHigher()
        {
            impMovementService.CurrentDirection = MovingObject.Direction.Horizontal;
            impAudioService.Play(SoundReferences.ImpGoing);
            impAnimationService.PlayWalkingAnimation(impTrainingService.Type);
            impTrainingService.IsTrainable = true;
        }

        

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            //
        }
    }
}