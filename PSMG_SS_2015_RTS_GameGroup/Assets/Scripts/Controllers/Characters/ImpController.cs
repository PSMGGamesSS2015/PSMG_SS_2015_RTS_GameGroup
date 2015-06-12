using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using Assets.Scripts.Types;
using Assets.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters
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
        private ImpSelection impSelection;
        private float movementSpeed = 0.6f;
        private float formerMovementSpeed;
        private bool facingRight = true;
        private bool movingUpwards = false;
        //profession-related
        private TriggerCollider2D attackRange;
        private List<TrollController> enemiesInAttackRange;
        private ImpController commandPartner;
        private bool isAtThrowingPosition;
        //general
        private ImpType type;
        private List<ImpControllerListener> listeners;
        public LayerMask impLayer;
        //prefabs
        public GameObject verticalLadderPrefab;
        public GameObject horizontalLadderPrefab;
        public GameObject counter;
        private ImpInventory impInventory;
        // ui
        private bool areLabelsDisplayed;
        private bool isPlacingLadder;
        // constants
        private const float MOVEMENT_SPEED_WALKING = 0.6f;
        private const float MOVEMENT_SPEED_RUNNING = 1.8f;
        private Counter bombCounter;
        private Counter attackCounter;
        private bool isTrainable;
        private AudioHelper audioHelper;

        #endregion

        # region listener interface

        public interface ImpControllerListener
        {
            void OnImpSelected(ImpController impController);
            void OnImpHurt(ImpController impController);
            void OnUntrain(ImpController impController);
        }

        public void RegisterListener(ImpControllerListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ImpControllerListener listener)
        {
            listeners.Remove(listener);
        }

        #endregion

        #region initialization, properties, input handling and update

        private void OnMouseDown()
        {
            foreach (ImpControllerListener listener in listeners)
            {
                listener.OnImpSelected(this);
            }
        }

        private void OnGUI()
        {
            if (areLabelsDisplayed)
            {
                Handles.Label(new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.75f, 0), type.ToString());
            }
        }

        public void MoveToSortingLayerPosition(int position)
        {
            foreach (SpriteRenderer r in sprites)
            {
                r.sortingOrder = position;
            }
        }

        private void Awake()
        {
            InitComponents();
            InitAttributes();
            InitTriggerColliders();
            animator.Play(AnimationReferences.ImpWalkingUnemployed);
        }

        private void InitComponents()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            animator = GetComponent<Animator>();
            impInventory = GetComponentInChildren<ImpInventory>();
            sprites = GetComponentsInChildren<SpriteRenderer>();
            impSelection = GetComponentInChildren<ImpSelection>();
            audioHelper = GetComponent<AudioHelper>();
        }

        private void InitAttributes()
        {
            isAtThrowingPosition = false;
            type = ImpType.Unemployed;
            isPlacingLadder = false;
            isTrainable = true;
            listeners = new List<ImpControllerListener>();
        }

        private void InitTriggerColliders()
        {
            TriggerCollider2D[] triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

            foreach (TriggerCollider2D c in triggerColliders)
            {
                if (c.tag == TagReferences.ImpAttackRange)
                {
                    attackRange = c;
                }
                else if (c.tag == TagReferences.ImpCollisionCheck)
                {
                    impCollisionCheck = c;
                }
                else if(c.tag == TagReferences.ImpClickCheck) {
                    impClickCheck = c;
                }
            }
            impCollisionCheck.RegisterListener(this);
            attackRange.RegisterListener(this);
            impClickCheck.RegisterListener(this);

            enemiesInAttackRange = new List<TrollController>();
        }

        public bool IsPlacingLadder
        {
            get
            {
                return isPlacingLadder;
            }
        }

        public ImpType Type
        {
            get
            {
                return type;
            }
        }

        public ImpSelection Selection
        {
            get
            {
                return impSelection;
            }
        }

        public bool IsTrainable
        {
            get
            {
                return isTrainable;
            }
        }

        private void FixedUpdate()
        {
        
            if (type != ImpType.Coward && 
                !IsInCommand())
            {
                if (movingUpwards)
                {
                    MoveUpwards();
                }
                else
                {
                    Move();
                }   
            }

        }

        public void LeaveGame()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
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
            if (type == ImpType.Spearman)
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
            Instantiate(verticalLadderPrefab, position, Quaternion.identity);
            Untrain();
        }

        private void SetupHorizontalLadder(Vector3 position)
        {
            StartCoroutine(SetupHorizontalLadderRoutine(position)); 
        }

        private IEnumerator SetupHorizontalLadderRoutine(Vector3 position)
        {
            float formerMovementSpeed = movementSpeed;
            movementSpeed = 0f;
            isPlacingLadder = true;
        
            impInventory.DisplayLadder();
        
            animator.Play(AnimationReferences.ImpPlacingLadderHorizontally);
            audioHelper.Play(SoundReferences.ImpSetupLadder);

            yield return new WaitForSeconds(5.5f);
  
            impInventory.HideAllTools();
            isPlacingLadder = false;
            Untrain();
            animator.Play(AnimationReferences.ImpWalkingUnemployed);
            StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
            Instantiate(horizontalLadderPrefab, new Vector3(position.x + 0.6f, position.y, 0), Quaternion.Euler(0, 0, -90));
        }

        private void StartMovingAgain(float direction, float speed)
        {
            movementSpeed = (direction < 0f) ? -speed : speed;
        }

        private void FormCommand(ImpController commandPartner)
        {
            if (type == ImpType.Spearman)
            {
                animator.Play(AnimationReferences.ImpStandingWithSpear);
                attackCounter = Instantiate(counter).GetComponent<Counter>();
                attackCounter.Init(4f, Pierce, true);
            }
            this.commandPartner = commandPartner;
        }

        public void DissolveCommand()
        {
            if (type == ImpType.Spearman)
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
            float formerMovementSpeed = movementSpeed;
            bool isFlippingNecessary = (formerMovementSpeed < 0);
            movementSpeed = 0f;

            if (isFlippingNecessary)
            {
                //Flip(impInventory.Explo.gameObject);
                impInventory.Explo.Flip();
            }

            impInventory.DisplayExplosion();
            animator.Play(AnimationReferences.ImpDetonatingBomb);
            audioHelper.Play(SoundReferences.BombExplosion);
        
            yield return new WaitForSeconds(1f);

            if (isFlippingNecessary)
            {
                // Flip(impInventory.Explo.gameObject);
                impInventory.Explo.Flip();
            }

            Collider2D[] objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
            foreach (Collider2D c in objectsWithinRadius)
            {
                if (c.gameObject.tag == TagReferences.Obstacle)
                {
                    Destroy(c.gameObject);
                }
            }

            StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
            isTrainable = true;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            string tag = collision.gameObject.tag;

            switch (tag)
            {
                case TagReferences.EnemyTroll:
                    TrollController enemy = collision.gameObject.GetComponent<TrollController>();
                    InteractWith(enemy);
                    break;
                case TagReferences.Imp:
                    ImpController imp = collision.gameObject.GetComponent<ImpController>();
                    InteractWith(imp);
                    break;
                case TagReferences.Obstacle:
                    ObstacleController obstacle = collision.gameObject.GetComponent<ObstacleController>();
                    InteractWith(obstacle);
                    break;
                case TagReferences.Impassable:
                    Turn();
                    break;
            
                default:
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            string tag = collider.gameObject.tag;

            switch (tag)
            {
                case TagReferences.LadderSpotVertical:
                    if (type == ImpType.LadderCarrier)
                    {
                        LadderSpotController ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
                        if (!ladderSpotController.IsLadderPlaced)
                        {
                            SetupVerticalLadder(collider.gameObject.transform.position);
                            ladderSpotController.PlaceLadder();
                        }
                    }
                    break;
                case TagReferences.LadderSpotHorizontal:
                    if (type == ImpType.LadderCarrier)
                    {
                        LadderSpotController ladderSpotController = collider.gameObject.GetComponent<LadderSpotController>();
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
                default:
                    break;
            }
        }

        private void PlayWalkingAnimation()
        {
            string anim;
            if (type == ImpType.Spearman)
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

        private void OnCollisionStay2D(Collision2D collision)
        {
            ImpController imp = collision.gameObject.GetComponent<ImpController>();
            if (imp != null)
            {
                if (imp.Type == ImpType.Coward)
                {
                    if (type != ImpType.Spearman)
                    {
                        Turn();
                    }
                }
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
                ((type == ImpType.Coward && imp.Type == ImpType.Spearman) ||
                 (type == ImpType.Spearman && imp.Type == ImpType.Coward)))
            {
                FormCommand(imp);
            }

            else if ((imp.Type == ImpType.Coward || imp.IsPlacingLadder)  &&
                     (type == ImpType.Unemployed ||
                      type == ImpType.LadderCarrier ||
                      type == ImpType.Blaster ||
                      type == ImpType.Firebug ||
                      type == ImpType.Botcher ||
                      type == ImpType.Schwarzenegger))
            {
                Turn();
            }

            else if ((type == ImpType.Schwarzenegger) &&
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
            if (this.type != ImpType.Coward)
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
            if (this.type == ImpType.Spearman || this.type == ImpType.Coward)
            {
                DissolveCommand();
            }

            this.type = type;

            if (type == ImpType.Blaster) TrainBlaster(formerMovementSpeed);
        
            if (type == ImpType.Spearman) TrainSpearman(formerMovementSpeed);

            if (type == ImpType.LadderCarrier) TrainLadderCarrier(formerMovementSpeed);

            if (type == ImpType.Unemployed) TrainUnemployed(formerMovementSpeed);

            if (type == ImpType.Coward) TrainCoward();

        }

        private void TrainUnemployed(float formerMovementSpeed)
        {
            impInventory.HideAllTools();
            if (formerMovementSpeed < 0)
            {
                movementSpeed = -0.6f;
            }
            else if (formerMovementSpeed == 0)
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
            StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
            animator.Play(AnimationReferences.ImpWalkingLadder);
        }

        private void TrainSpearman(float formerMovementSpeed)
        {
            impInventory.Display(TagReferences.ImpInventorySpear);
            StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
            animator.Play(AnimationReferences.ImpWalkingSpear);
        }

        private void TrainBlaster(float formerMovementSpeed)
        {
            isTrainable = false;
            SetupBombCounter();
            DisplayBlasterAnimation(formerMovementSpeed);
        }

        private void SetupBombCounter()
        {
            if (bombCounter != null) Destroy(bombCounter.gameObject);

            bombCounter = Instantiate(counter).GetComponent<Counter>();
            bombCounter.Init(3f, DetonateBomb, false);
        }

        private void DisplayBlasterAnimation(float formerMovementSpeed)
        {
            impInventory.Display(TagReferences.ImpInventoryBomb);
            StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_RUNNING);
            animator.Play(AnimationReferences.ImpWalkingBomb);
        }

        public bool HasJob()
        {
            return type != ImpType.Unemployed;
        }

        public void Untrain()
        {
            foreach (ImpControllerListener listener in listeners)
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
                ImpController imp = collider.gameObject.GetComponent<ImpController>();

                if (imp != null)
                {
                    Physics2D.IgnoreCollision(GetCollider(), imp.GetCollider(), false);
                }
            }

            if (self.GetInstanceID() == attackRange.GetInstanceID())
            {
                if (type == ImpType.Spearman)
                {
                    if (collider.gameObject.tag == TagReferences.EnemyTroll)
                    {
                        enemiesInAttackRange.Remove(collider.gameObject.GetComponent<TrollController>());
                    }
                }
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() == attackRange.GetInstanceID())
            {
                if (type == ImpType.Spearman)
                {
                    if (collider.gameObject.tag == TagReferences.EnemyTroll)
                    {
                        enemiesInAttackRange.Add(collider.gameObject.GetComponent<TrollController>());
                    }
                }
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
