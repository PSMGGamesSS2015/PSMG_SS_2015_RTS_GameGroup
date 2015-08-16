using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpCollisionService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private CircleCollider2D circleCollider2D;
        private TriggerCollider2D impCollisionCheck;
        private TriggerCollider2D impClickCheck;
        private ImpAnimationHelper impAnimationService;
        private ImpMovementService impMovementService;
        private ImpTrainingService impTrainingService;

        private List<Collider2D> collidersIgnoredWhileClimbing;

        public void Awake()
        {
            InitComponents();
            InitTriggerColliders();
        }

        private void InitComponents()
        {
            collidersIgnoredWhileClimbing = new List<Collider2D>();

            circleCollider2D = GetComponent<CircleCollider2D>();
            impAnimationService = GetComponent<ImpAnimationHelper>();
            impMovementService = GetComponent<ImpMovementService>();
            impTrainingService = GetComponent<ImpTrainingService>();
        }

        private void InitTriggerColliders()
        {
            var triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

            impCollisionCheck = triggerColliders.First(tc => tc.gameObject.tag == TagReferences.ImpCollisionCheck);
            impClickCheck = triggerColliders.First(tc => tc.gameObject.tag == TagReferences.ImpClickCheck);

            impCollisionCheck.RegisterListener(this);
            impClickCheck.RegisterListener(this);
        }

        public CircleCollider2D GetCollider()
        {
            return circleCollider2D;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (GetComponent<ImpMovementService>().IsClimbing)
            {
                collidersIgnoredWhileClimbing.Add(collision.gameObject.GetComponent<Collider2D>());
                Physics2D.IgnoreCollision(circleCollider2D, collision.gameObject.GetComponent<Collider2D>(), true);
                return;
            }

            switch (collision.gameObject.tag)
            {
                case TagReferences.EnemyTroll:
                    impMovementService.Turn();
                    break;
                case TagReferences.Imp:
                    var imp = collision.gameObject.GetComponent<ImpController>();
                    GetComponent<ImpInteractionLogicService>().OnCollisionEnterWithImp(imp);
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
            var tag = collision.gameObject.tag;

            switch (tag)
            {
                case TagReferences.Imp:
                    OnCollisionStayWithImp(collision.gameObject.GetComponent<ImpController>());
                    break;
            }
        }

        private void OnCollisionStayWithImp(ImpController imp)
        {
            CheckIfSpearmanLeavesCommand(imp);
        }

        private void CheckIfSpearmanLeavesCommand(ImpController imp)
        {
            if (imp.GetComponent<ImpTrainingService>().Type != ImpType.Coward) return;

            if (impTrainingService.Type == ImpType.Spearman) return;

            impMovementService.Turn();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != impCollisionCheck.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.LadderSpotVertical:
                    OnEnterVerticalLadderSpot(collider);
                    break;
                case TagReferences.LadderSpotHorizontal:
                    OnEnterHorizontalLadderSpot(collider);
                    break;
                case TagReferences.LadderBottom:
                    OnEnterLadderBottom();
                    break;
                case TagReferences.LadderTop:
                    OnEnterLadderTop();
                    break;
                case TagReferences.Gaslight:
                    OnEnterGaslight(collider);
                    break;
                case TagReferences.BurningObject:
                    OnEnterBurningObject(collider);
                    break;
                case TagReferences.SchwarzeneggerSpot:
                    OnEnterSchwarzeneggerSpot(collider);
                    break;
            }
        }

        private void OnEnterSchwarzeneggerSpot(Collider2D collider)
        {
            if (GetComponent<ImpSchwarzeneggerService>() == null) return;
            if (collider.GetComponent<SchwarzeneggerSpotController>() == null) return;
            GetComponent<ImpSchwarzeneggerService>().IsAtThrowingPosition = true;
            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpStanding);
        }

        private void OnEnterBurningObject(Collider2D collider)
        {
            if (GetComponent<ImpFirebugService>() == null) return;

            GetComponent<ImpFirebugService>().SetOnFire(collider.gameObject, 5);
        }

        private void OnEnterGaslight(Collider2D collider)
        {
            if (GetComponent<ImpFirebugService>() == null) return;

            GetComponent<ImpFirebugService>().LightGaslight(collider.gameObject.GetComponent<GaslightController>());
        }

        private void OnEnterLadderTop()
        {
            if (!GetComponent<ImpMovementService>().IsClimbing) return;

            impMovementService.ClimbALittleHigher();
            impAnimationService.Play(AnimationReferences.ImpClimbingLadderEnd);
        }

        private void OnEnterLadderBottom()
        {
            if (!GetComponent<ImpMovementService>().FacingRight ||
                GetComponent<ImpTrainingService>().Type == ImpType.Coward) return;

            GetComponent<ImpMovementService>().ClimbLadder();
            impTrainingService.IsTrainable = false;
            GetComponent<ImpMovementService>().IsClimbing = true;
        }

        private void OnEnterHorizontalLadderSpot(Collider2D collider)
        {
            if (impTrainingService.Type != ImpType.LadderCarrier) return;

            var ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
            GetComponent<ImpLadderCarrierService>().SetupHorizontalLadder(ladderSpotController);
        }

        private void OnEnterVerticalLadderSpot(Collider2D collider)
        {
            if (impTrainingService.Type != ImpType.LadderCarrier) return;

            var ladderSpotController = collider.gameObject.GetComponent<VerticalLadderSpotController>();
            GetComponent<ImpLadderCarrierService>().SetupVerticalLadder(ladderSpotController);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != impCollisionCheck.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerExitImp(collider.gameObject.GetComponent<ImpController>());
                    break;
                case TagReferences.LadderMiddle:
                    OnTriggerExitLadderMiddle();
                    break;
            }
        }

        private void OnTriggerExitLadderMiddle()
        {
            if (GetComponent<ImpMovementService>().IsClimbing)
            {
                GetComponent<ImpSpriteManagerService>().MoveToSortingLayer(SortingLayerReferences.MiddleForeground);
            }
        }

        public void StopIgnoringCollisions()
        {
            foreach (var ci in collidersIgnoredWhileClimbing)
            {
                Physics2D.IgnoreCollision(circleCollider2D, ci, false);
            }
            collidersIgnoredWhileClimbing.Clear();
        }

        private void OnTriggerExitImp(ImpController imp)
        {
            Physics2D.IgnoreCollision(GetCollider(), imp.GetComponent<ImpCollisionService>().GetCollider(), false);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            //
        }


    }
}