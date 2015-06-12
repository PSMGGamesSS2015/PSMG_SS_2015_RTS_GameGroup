using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEditor;
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
        private Animator animator;
        // movement
        private Rigidbody2D rigidBody2D;
        private CircleCollider2D circleCollider2D;
        private TriggerCollider2D impCollisionCheck;
        private TriggerCollider2D impClickCheck;
        private SpriteRenderer[] sprites;
        private float movementSpeed = 0.6f;
        private float formerMovementSpeed;
        private bool facingRight = true;
        private bool movingUpwards;
        //profession-related
        private TriggerCollider2D attackRange;
        private List<TrollController> enemiesInAttackRange;
        private ImpController commandPartner;
        private bool isAtThrowingPosition;
        //general
        private List<IImpControllerListener> listeners;
        public LayerMask impLayer;
        //prefabs
        public GameObject VerticalLadderPrefab;
        public GameObject HorizontalLadderPrefab;
        private ImpInventory impInventory;
        // ui
        private bool areLabelsDisplayed;
        // constants
        private const float MovementSpeedWalking = 0.6f;
        private const float MovementSpeedRunning = 1.8f;
        private Counter bombCounter;
        private Counter attackCounter;
        private AudioHelper audioHelper;
        // services
        private MovingObject movementService;
        private ImpTrainingService impTrainingService;

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
            listeners.Add(listener);
        }

        public void UnregisterListener(IImpControllerListener listener)
        {
            listeners.Remove(listener);
        }

        #endregion

        #region initialization, properties, input handling and update

        public void OnMouseDown()
        {
            foreach (var listener in listeners)
            {
                listener.OnImpSelected(this);
            }
        }

        private void OnGUI()
        {
            if (areLabelsDisplayed)
            {
                Handles.Label(new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.75f, 0), Type.ToString());
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
            animator.Play(AnimationReferences.ImpWalkingUnemployed);
        }

        private void InitServices()
        {
            //movementService = gameObject.AddComponent<MovingObject>();
            impTrainingService = gameObject.AddComponent<ImpTrainingService>();
        }

        private void InitComponents()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            animator = GetComponent<Animator>();
            impInventory = GetComponentInChildren<ImpInventory>();
            sprites = GetComponentsInChildren<SpriteRenderer>();
            Selection = GetComponentInChildren<ImpSelection>();
            audioHelper = GetComponent<AudioHelper>();
        }

        private void InitAttributes()
        {
            isAtThrowingPosition = false;
            Type = ImpType.Unemployed;
            IsPlacingLadder = false;
            IsTrainable = true;
            listeners = new List<IImpControllerListener>();
        }

        private void InitTriggerColliders()
        {
            var triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

            foreach (var c in triggerColliders)
            {
                switch (c.tag)
                {
                    case TagReferences.ImpAttackRange:
                        attackRange = c;
                        break;
                    case TagReferences.ImpCollisionCheck:
                        impCollisionCheck = c;
                        break;
                    case TagReferences.ImpClickCheck:
                        impClickCheck = c;
                        break;
                }
            }
            impCollisionCheck.RegisterListener(this);
            attackRange.RegisterListener(this);
            impClickCheck.RegisterListener(this);

            enemiesInAttackRange = new List<TrollController>();
        }

        public void Start()
        {
            //movementService.StartMoving(true, MovingObject.Direction.Horizontal);
        }

        public bool IsPlacingLadder { get; private set; }

        public ImpType Type { get; private set; }

        public ImpSelection Selection { get; private set; }

        public bool IsTrainable { get; private set; }

        public void FixedUpdate()
        {
            if (Type == ImpType.Coward || IsInCommand()) return;
            if (movingUpwards)
            {
                MoveUpwards();
                //movementService.MoveUpwards();
            }
            else
            {
                Move();
                //movementService.Move();
            }
        }

        public void LeaveGame()
        {
            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnImpHurt(this);
            }
            listeners.Clear();
            this.StopAllCounters();
            Destroy(gameObject);
        }

        #endregion

        #region basic movement patterns

        private void Move()
        {
            rigidBody2D.velocity = new Vector2(movementSpeed, rigidBody2D.velocity.y);
        }
    
        private void Turn()
        {
            movementSpeed *= -1;
            facingRight = !facingRight;
            this.Flip();
        }

        private void MoveUpwards()
        {
            rigidBody2D.velocity = new Vector2(0f, 1f); 
        }

        private void ClimbLadder()
        {
            PlayClimbingAnimation();
            movingUpwards = true;
        }

        private void PlayClimbingAnimation()
        {
            string anim;
            if (Type == ImpType.Spearman)
            {
                anim = AnimationReferences.ImpClimbingLadderSpearman;
            }
            else
            {
                anim = AnimationReferences.ImpClimbingLadderUnemployed;
            }
            animator.Play(anim);
        }

        #endregion

        #region profession-specific methods

        private void Pierce()
        {
            StartCoroutine(PiercingRoutine());
        }

        private IEnumerator PiercingRoutine()
        {
            animator.Play(AnimationReferences.ImpAttackingWithSpear);
            audioHelper.Play(SoundReferences.ImpAttack1);

            yield return new WaitForSeconds(0.75f);

            foreach (TrollController enemy in enemiesInAttackRange)
            {
                enemy.ReceiveHit();
            }
            enemiesInAttackRange.Clear();

            animator.Play(AnimationReferences.ImpStandingWithSpear);
        }

        private void SetupVerticalLadder(Vector3 position)
        {
            Instantiate(VerticalLadderPrefab, position, Quaternion.identity);
            Untrain();
        }

        private void SetupHorizontalLadder(Vector3 position)
        {
            StartCoroutine(SetupHorizontalLadderRoutine(position)); 
        }

        private IEnumerator SetupHorizontalLadderRoutine(Vector3 position)
        {
            var formerMovementSpeed = movementSpeed;
            movementSpeed = 0f;
            IsPlacingLadder = true;
        
            impInventory.DisplayLadder();
        
            animator.Play(AnimationReferences.ImpPlacingLadderHorizontally);
            audioHelper.Play(SoundReferences.ImpSetupLadder);

            yield return new WaitForSeconds(5.5f);
  
            impInventory.HideAllTools();
            IsPlacingLadder = false;
            Untrain();
            animator.Play(AnimationReferences.ImpWalkingUnemployed);
            StartMovingAgain(formerMovementSpeed, MovementSpeedWalking);
            Instantiate(HorizontalLadderPrefab, new Vector3(position.x + 0.6f, position.y, 0), Quaternion.Euler(0, 0, -90));
        }

        private void StartMovingAgain(float direction, float speed)
        {
            movementSpeed = (direction < 0f) ? -speed : speed;
        }

        private void FormCommand(ImpController commandPartner)
        {
            if (Type == ImpType.Spearman)
            {
                animator.Play(AnimationReferences.ImpStandingWithSpear);
                attackCounter = Counter.SetCounter(this.gameObject, 4f, Pierce, true);;
            }
            this.commandPartner = commandPartner;
        }

        public void DissolveCommand()
        {
            if (Type == ImpType.Spearman)
            {
                animator.Play(AnimationReferences.ImpWalkingSpear);
                if (attackCounter != null)
                {
                    attackCounter.Stop();
                }
            
            }
            this.commandPartner = null;
        }

        public bool IsInCommand()
        {
            return commandPartner != null;
        }

        private void DetonateBomb()
        {
            StartCoroutine(DetonatingRoutine());  
        }

        private IEnumerator DetonatingRoutine()
        {
            var formerMovementSpeed = movementSpeed;
            var isFlippingNecessary = (formerMovementSpeed < 0);
            movementSpeed = 0f;

            if (isFlippingNecessary)
            {
                impInventory.Explo.Flip();
            }

            impInventory.DisplayExplosion();
            animator.Play(AnimationReferences.ImpDetonatingBomb);
            audioHelper.Play(SoundReferences.BombExplosion);
        
            yield return new WaitForSeconds(1f);

            if (isFlippingNecessary)
            {
                impInventory.Explo.Flip();
            }

            var objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
            foreach (var c in objectsWithinRadius.Where(c => c.gameObject.tag == TagReferences.Obstacle))
            {
                Destroy(c.gameObject);
            }

            StartMovingAgain(formerMovementSpeed, MovementSpeedWalking);
            IsTrainable = true;
            Untrain();
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
                    var enemy = collision.gameObject.GetComponent<TrollController>();
                    InteractWith(enemy);
                    break;
                case TagReferences.Imp:
                    var imp = collision.gameObject.GetComponent<ImpController>();
                    InteractWith(imp);
                    break;
                case TagReferences.Obstacle:
                    var obstacle = collision.gameObject.GetComponent<ObstacleController>();
                    InteractWith(obstacle);
                    break;
                case TagReferences.Impassable:
                    Turn();
                    break;
            }
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var tag = collider.gameObject.tag;

            switch (tag)
            {
                case TagReferences.LadderSpotVertical:
                    if (Type == ImpType.LadderCarrier)
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
                    if (Type == ImpType.LadderCarrier)
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
                    movingUpwards = false;
                    PlayWalkingAnimation();
                    break;
            }
        }

        private void PlayWalkingAnimation()
        {
            string anim;
            if (Type == ImpType.Spearman)
            {
                anim = AnimationReferences.ImpWalkingSpear;
            }
            else
            {
                anim = AnimationReferences.ImpWalkingUnemployed;
            }
            animator.Play(anim);
            audioHelper.Play(SoundReferences.ImpGoing);
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            var imp = collision.gameObject.GetComponent<ImpController>();
            if (imp == null) return;
            if (imp.Type != ImpType.Coward) return;
            if (Type != ImpType.Spearman)
            {
                Turn();
            }
        }

        #endregion

        #region interaction logic

        private void InteractWith(ObstacleController obstacle)
        {
            Turn();
            // TODO
        }

        private void InteractWith(TrollController enemy)
        {
            Turn();
            // TODO
        }

        private void InteractWith(ImpController imp)
        {
            if (commandPartner == null &&
                ((Type == ImpType.Coward && imp.Type == ImpType.Spearman) ||
                 (Type == ImpType.Spearman && imp.Type == ImpType.Coward)))
            {
                FormCommand(imp);
            }

            else if ((imp.Type == ImpType.Coward || imp.IsPlacingLadder)  &&
                     (Type == ImpType.Unemployed ||
                      Type == ImpType.LadderCarrier ||
                      Type == ImpType.Blaster ||
                      Type == ImpType.Firebug ||
                      Type == ImpType.Botcher ||
                      Type == ImpType.Schwarzenegger))
            {
                Turn();
            }

            else if ((Type == ImpType.Schwarzenegger) &&
                     ((imp.Type != ImpType.Schwarzenegger) ||
                      (imp.Type != ImpType.Coward)))
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

        #region training

        public void Train(ImpType type)
        {
            StartCoroutine(TrainingRoutine(type)); 
        }

        private IEnumerator TrainingRoutine(ImpType type)
        {
            if (this.Type != ImpType.Coward)
            {
                formerMovementSpeed = movementSpeed;
                movementSpeed = 0f;
            }

            impInventory.HideAllTools();
            animator.Play(AnimationReferences.ImpTakingObject);
            audioHelper.Play(SoundReferences.ImpSelect4);

            yield return new WaitForSeconds(1.0f);

            if (commandPartner != null)
            {
                commandPartner.DissolveCommand();
            }
            if (this.Type == ImpType.Spearman || this.Type == ImpType.Coward)
            {
                DissolveCommand();
            }

            this.Type = type;

            if (type == ImpType.Blaster) TrainBlaster(formerMovementSpeed);
        
            if (type == ImpType.Spearman) TrainSpearman(formerMovementSpeed);

            if (type == ImpType.LadderCarrier) TrainLadderCarrier(formerMovementSpeed);

            if (type == ImpType.Unemployed) TrainUnemployed(formerMovementSpeed);

            if (type == ImpType.Coward) TrainCoward();

        }

        private void TrainUnemployed(float formerMovementSpeed)
        {
            impInventory.HideAllTools();
            if (formerMovementSpeed < 0f)
            {
                movementSpeed = -0.6f;
            }
            else if (formerMovementSpeed == 0f)
            {
                if (facingRight)
                {
                    movementSpeed = 0.6f;
                }
                else
                {
                    movementSpeed = -0.6f;
                }
            }
            else
            {
                movementSpeed = 0.6f;
            }

            animator.Play(AnimationReferences.ImpWalkingUnemployed);
        }

        private void TrainCoward()
        {
            impInventory.Display(TagReferences.ImpInventoryShield);
            animator.Play(AnimationReferences.ImpHidingBehindShield);
            audioHelper.Play(SoundReferences.ShieldWood1);
        }

        private void TrainLadderCarrier(float formerMovementSpeed)
        {
            impInventory.Display(TagReferences.ImpInventoryLadder);
            StartMovingAgain(formerMovementSpeed, MovementSpeedWalking);
            animator.Play(AnimationReferences.ImpWalkingLadder);
        }

        private void TrainSpearman(float formerMovementSpeed)
        {
            impInventory.Display(TagReferences.ImpInventorySpear);
            StartMovingAgain(formerMovementSpeed, MovementSpeedWalking);
            animator.Play(AnimationReferences.ImpWalkingSpear);
        }

        private void TrainBlaster(float formerMovementSpeed)
        {
            IsTrainable = false;
            SetupBombCounter();
            DisplayBlasterAnimation(formerMovementSpeed);
        }

        private void SetupBombCounter()
        {
            if (bombCounter != null) Destroy(bombCounter.gameObject);
            bombCounter = Counter.SetCounter(this.gameObject, 3f, DetonateBomb, false);
        }

        private void DisplayBlasterAnimation(float formerMovementSpeed)
        {
            impInventory.Display(TagReferences.ImpInventoryBomb);
            StartMovingAgain(formerMovementSpeed, MovementSpeedRunning);
            animator.Play(AnimationReferences.ImpWalkingBomb);
        }

        public bool HasJob()
        {
            return Type != ImpType.Unemployed;
        }

        public void Untrain()
        {
            foreach (var listener in listeners)
            {
                listener.OnUntrain(this);
            }
        }

        #endregion

        # region interface implementation

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() == impCollisionCheck.GetInstanceID())
            {
                var imp = collider.gameObject.GetComponent<ImpController>();

                if (imp != null)
                {
                    Physics2D.IgnoreCollision(GetCollider(), imp.GetCollider(), false);
                }
            }

            if (self.GetInstanceID() != attackRange.GetInstanceID()) return;
            if (Type != ImpType.Spearman) return;
            if (collider.gameObject.tag == TagReferences.EnemyTroll)
            {
                enemiesInAttackRange.Remove(collider.gameObject.GetComponent<TrollController>());
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != attackRange.GetInstanceID()) return;
            if (Type != ImpType.Spearman) return;
            if (collider.gameObject.tag == TagReferences.EnemyTroll)
            {
                enemiesInAttackRange.Add(collider.gameObject.GetComponent<TrollController>());
            }
        }

        #endregion


        public void DisplayLabel()
        {
            areLabelsDisplayed = true;
        }

        public void DismissLabel()
        {
            areLabelsDisplayed = false;
        }
    }
}
