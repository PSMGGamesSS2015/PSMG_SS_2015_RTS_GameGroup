using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    /// <summary>
    /// The ImpController is a component attached to every instance of
    /// an Imp prefab. It manages movement patterns and collision detection
    /// of imps and listens for click events on the imps.
    /// </summary>

    public class ImpController : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        #region variables and constants
        
        // animation
        
        // movement
        private CircleCollider2D circleCollider2D;
        private TriggerCollider2D impCollisionCheck;
        private TriggerCollider2D impClickCheck;
        private SpriteRenderer[] sprites;
        //profession-related
        
        private bool isAtThrowingPosition;
        //general
        public List<IImpControllerListener> Listeners;
        //prefabs
        public GameObject VerticalLadderPrefab;
        public GameObject HorizontalLadderPrefab;
        // constants
        public Counter bombCounter;

        public AudioHelper ImpAudioService;
        public ImpMovementService ImpMovementService;
        public ImpTrainingService ImpTrainingService;
        public ImpAnimationHelper ImpAnimationService;
        public ImpUIService ImpUIService;
        

        #endregion

        # region listener interface

        public interface IImpControllerListener
        {
            void OnImpSelected(ImpController impController);
            void OnImpHurt(ImpController impController);
            void OnUntrain(ImpController impController);
        }

        public void RegisterListener(IImpControllerListener listener)
        {
            Listeners.Add(listener);
        }

        public void UnregisterListener(IImpControllerListener listener)
        {
            Listeners.Remove(listener);
        }

        #endregion

        #region initialization, properties, input handling and update

        public void OnMouseDown()
        {
            foreach (var listener in Listeners)
            {
                listener.OnImpSelected(this);
            }
        }

        

        public void MoveToSortingLayerPosition(int position)
        {
            foreach (var r in sprites)
            {
                r.sortingOrder = position;
            }
        }

        public void Awake()
        {
            InitComponents();
            InitServices();
            InitAttributes();
            InitTriggerColliders();
            ImpAnimationService.Play(AnimationReferences.ImpWalkingUnemployed);
        }

        private void InitServices()
        {
            ImpMovementService = gameObject.AddComponent<ImpMovementService>();
            ImpTrainingService = gameObject.AddComponent<ImpTrainingService>();
            ImpUIService = gameObject.AddComponent<ImpUIService>();
        }

        private void InitComponents()
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
            sprites = GetComponentsInChildren<SpriteRenderer>();
            Selection = GetComponentInChildren<ImpSelection>();
            ImpAudioService = GetComponent<AudioHelper>();
            ImpAnimationService = GetComponent<ImpAnimationHelper>();
        }

        private void InitAttributes()
        {
            isAtThrowingPosition = false;
            ImpTrainingService.Type = ImpType.Unemployed;
            IsPlacingLadder = false;
            ImpTrainingService.IsTrainable = true;
            Listeners = new List<IImpControllerListener>();
        }

        private void InitTriggerColliders()
        {
            var triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

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

        public bool IsPlacingLadder { get; private set; }

        public ImpSelection Selection { get; private set; }

        public void LeaveGame()
        {
            for (var i = Listeners.Count - 1; i >= 0; i--)
            {
                Listeners[i].OnImpHurt(this);
            }
            Listeners.Clear();
            this.StopAllCounters();
            Destroy(gameObject);
        }

        #endregion

        #region basic movement patterns

        private void ClimbLadder()
        {
            PlayClimbingAnimation();
            ImpMovementService.MoveUpwards();
        }

        private void PlayClimbingAnimation()
        {
            string anim;
            if (ImpTrainingService.Type == ImpType.Spearman)
            {
                anim = AnimationReferences.ImpClimbingLadderSpearman;
            }
            else
            {
                anim = AnimationReferences.ImpClimbingLadderUnemployed;
            }
            ImpAnimationService.Play(anim);
        }

        #endregion

        #region profession-specific methods

       private void SetupVerticalLadder(Vector3 position)
        {
            Instantiate(VerticalLadderPrefab, position, Quaternion.identity);
            ImpTrainingService.Untrain();
        }

        private void SetupHorizontalLadder(Vector3 position)
        {
            StartCoroutine(SetupHorizontalLadderRoutine(position)); 
        }

        private IEnumerator SetupHorizontalLadderRoutine(Vector3 position)
        {
            ImpMovementService.Stand();
            IsPlacingLadder = true;
        
            ImpAnimationService.Play(AnimationReferences.ImpPlacingLadderHorizontally);
            ImpAudioService.Play(SoundReferences.ImpSetupLadder);

            yield return new WaitForSeconds(5.5f);

            ImpAnimationService.SwitchBackToStandardAnimation();
            IsPlacingLadder = false;
            ImpTrainingService.Untrain();
            ImpMovementService.Walk();
            Instantiate(HorizontalLadderPrefab, new Vector3(position.x + 0.6f, position.y, 0), Quaternion.Euler(0, 0, -90));
        }

       

        public void DetonateBomb()
        {
            StartCoroutine(DetonatingRoutine());  
        }

        private IEnumerator DetonatingRoutine()
        {
            var formerMovementSpeed = ImpMovementService.movementSpeed;
            var isFlippingNecessary = (formerMovementSpeed < 0);
            ImpMovementService.Stand();

            if (isFlippingNecessary)
            {
                ImpAnimationService.ImpInventory.Explosion.Flip();
            }

            ImpAnimationService.ImpInventory.DisplayExplosion();
            ImpAnimationService.Play(AnimationReferences.ImpDetonatingBomb);
            ImpAudioService.Play(SoundReferences.BombExplosion);
        
            yield return new WaitForSeconds(1f);

            if (isFlippingNecessary)
            {
                ImpAnimationService.ImpInventory.Explosion.Flip();
            }

            var objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
            foreach (var c in objectsWithinRadius.Where(c => c.gameObject.tag == TagReferences.Obstacle))
            {
                Destroy(c.gameObject);
            }

            ImpMovementService.Walk();
            ImpTrainingService.IsTrainable = true;
            ImpTrainingService.Untrain();
        }

        private void ThrowImp(ImpController projectile)
        {
            // TODO 
        }

        #endregion 

        #region collision management and related behaviors

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
                    ImpMovementService.Turn();
                    break;
                case TagReferences.Imp:
                    var imp = collision.gameObject.GetComponent<ImpController>();
                    InteractWith(imp);
                    break;
                case TagReferences.Obstacle:
                    ImpMovementService.Turn();
                    break;
                case TagReferences.Impassable:
                    ImpMovementService.Turn();
                    break;
            }
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var tag = collider.gameObject.tag;

            switch (tag)
            {
                case TagReferences.LadderSpotVertical:
                    if (ImpTrainingService.Type == ImpType.LadderCarrier)
                    {
                        var ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
                        if (!ladderSpotController.IsLadderPlaced)
                        {
                            SetupVerticalLadder(collider.gameObject.transform.position);
                            ladderSpotController.PlaceLadder();
                        }
                    }
                    break;
                case TagReferences.LadderSpotHorizontal:
                    if (ImpTrainingService.Type == ImpType.LadderCarrier)
                    {
                        var ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
                        if (!ladderSpotController.IsLadderPlaced)
                        {
                            SetupHorizontalLadder(collider.gameObject.transform.position);
                            ladderSpotController.PlaceLadder();
                        }
                    }
                    break;
                case TagReferences.LadderBottom:
                    ClimbLadder();
                    break;
                case TagReferences.LadderTop:
                    ImpMovementService.Move();
                    ImpAudioService.Play(SoundReferences.ImpGoing);
                    ImpAnimationService.PlayWalkingAnimation(ImpTrainingService.Type);
                    break;
            }
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            var imp = collision.gameObject.GetComponent<ImpController>();
            if (imp == null) return;
            if (imp.ImpTrainingService.Type != ImpType.Coward) return;
            if (ImpTrainingService.Type != ImpType.Spearman)
            {
                ImpMovementService.Turn();
            }
        }

        #endregion

        #region interaction logic

        private void InteractWith(ImpController imp)
        {

            // TODO refactor
            if (((ImpTrainingService.Type == ImpType.Spearman && imp.ImpTrainingService.Type == ImpType.Coward) ||
                 (ImpTrainingService.Type == ImpType.Coward && imp.ImpTrainingService.Type == ImpType.Spearman)))
            {

                if (((ImpTrainingService.Type == ImpType.Spearman) &&
                    GetComponent<ImpSpearmanService>().CommandPartner == null) ||
                    ((ImpTrainingService.Type == ImpType.Coward) &&
                    GetComponent<ImpCowardService>().CommandPartner == null)
                    )
                {
                    if (ImpTrainingService.Type == ImpType.Spearman)
                    {
                        GetComponent<ImpSpearmanService>().FormCommand(imp);
                    }
                    if (ImpTrainingService.Type == ImpType.Coward)
                    {
                        GetComponent<ImpCowardService>().FormCommand(imp);
                    }
                }
                
            }

            else if ((imp.ImpTrainingService.Type == ImpType.Coward || imp.IsPlacingLadder) &&
                     (ImpTrainingService.Type == ImpType.Unemployed ||
                      ImpTrainingService.Type == ImpType.LadderCarrier ||
                      ImpTrainingService.Type == ImpType.Blaster ||
                      ImpTrainingService.Type == ImpType.Firebug ||
                      ImpTrainingService.Type == ImpType.Botcher ||
                      ImpTrainingService.Type == ImpType.Schwarzenegger))
            {
                ImpMovementService.Turn();
            }

            else if ((ImpTrainingService.Type == ImpType.Schwarzenegger) &&
                     ((imp.ImpTrainingService.Type != ImpType.Schwarzenegger) ||
                      (imp.ImpTrainingService.Type != ImpType.Coward)))
            {
                if (isAtThrowingPosition)
                {
                    ThrowImp(imp);
                }
            }
            else
            {
                Physics2D.IgnoreCollision(GetCollider(), imp.GetCollider(), true);
            }
        }

        #endregion

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != impCollisionCheck.GetInstanceID()) return;
            
            var imp = collider.gameObject.GetComponent<ImpController>();

            if (imp != null)
            {
                Physics2D.IgnoreCollision(GetCollider(), imp.GetCollider(), false);
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
        }

    }
}
