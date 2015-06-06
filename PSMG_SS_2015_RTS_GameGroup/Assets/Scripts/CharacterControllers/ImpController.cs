using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

/// <summary>
/// The ImpController is a component attached to every instance of
/// an Imp prefab. It manages movement patterns and collision detection
/// of imps and listens for click events on the imps.
/// </summary>

public class ImpController : MonoBehaviour, TriggerCollider2D.TriggerCollider2DListener
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
    private bool facingRight = true;
    private bool movingUpwards = false;
    //profession-related
    private TriggerCollider2D attackRange;
    private float attackCounter = 0f;
    private List<EnemyController> enemiesInAttackRange;
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
    private Counter attackCounter1;

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
        animator.Play(AnimationReferences.IMP_WALKING_UNEMPLOYED);
    }

    private void InitComponents()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        impInventory = GetComponentInChildren<ImpInventory>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        impSelection = GetComponentInChildren<ImpSelection>();
    }

    private void InitAttributes()
    {
        isAtThrowingPosition = false;
        type = ImpType.Unemployed;
        isPlacingLadder = false;
        listeners = new List<ImpControllerListener>();
    }

    private void InitTriggerColliders()
    {
        TriggerCollider2D[] triggerColliders = GetComponentsInChildren<TriggerCollider2D>();

        foreach (TriggerCollider2D c in triggerColliders)
        {
            if (c.tag == "AttackRange")
            {
                attackRange = c;
            }
            else if (c.tag == "ImpCollisionCheck")
            {
                impCollisionCheck = c;
            }
            else if(c.tag == "ImpClickCheck") {
                impClickCheck = c;
            }
        }
        impCollisionCheck.RegisterListener(this);
        attackRange.RegisterListener(this);
        impClickCheck.RegisterListener(this);

        enemiesInAttackRange = new List<EnemyController>();
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
        foreach (ImpControllerListener listener in listeners)
        {
            listener.OnImpHurt(this);
        }
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
        Flip(gameObject);
    }

    private void Flip(GameObject obj)
    {
        Vector3 newScale = obj.transform.localScale;
        newScale.x *= -1;
        obj.transform.localScale = newScale;
    }

    private void MoveUpwards()
    {
        rigidBody2D.velocity = new Vector2(0f, 1f); 
        // TODO Stop moving upwards when reached top
    }

    private void ClimbLadder()
    {
        PlayClimbingAnimation();
        movingUpwards = true;
        // TODO check when the top is reached
    }

    private void PlayClimbingAnimation()
    {
        string anim;
        if (type == ImpType.Spearman)
        {
            anim = AnimationReferences.IMP_CLIMBING_LADDER_SPEARMAN;
        }
        else
        {
            anim = AnimationReferences.IMP_CLIMBING_LADDER_UNEMPLOYED;
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
        animator.Play(AnimationReferences.IMP_ATTACKING_WITH_SPEAR);

        yield return new WaitForSeconds(0.75f);

        foreach (EnemyController enemy in enemiesInAttackRange)
        {
            enemy.ReceiveHit();
        }
        enemiesInAttackRange.Clear();

        animator.Play(AnimationReferences.IMP_STANDING_WITH_SPEAR);
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
        
        animator.Play(AnimationReferences.IMP_PLACING_LADDER_HORIZONTALLY);

        yield return new WaitForSeconds(5.5f);
  
        impInventory.HideAllTools();
        isPlacingLadder = false;
        Untrain();
        animator.Play(AnimationReferences.IMP_WALKING_UNEMPLOYED);
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
            animator.Play(AnimationReferences.IMP_STANDING_WITH_SPEAR);
            attackCounter1 = Instantiate(counter).GetComponent<Counter>();
            attackCounter1.Init(4f, Pierce, true);
        }
        this.commandPartner = commandPartner;
    }

    public void DissolveCommand()
    {
        if (type == ImpType.Spearman)
        {
            animator.Play(AnimationReferences.IMP_WALKING_SPEAR);
            if (attackCounter1 != null)
            {
                attackCounter1.Stop();
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
            Flip(impInventory.Explo.gameObject);
        }

        impInventory.DisplayExplosion();
        animator.Play(AnimationReferences.IMP_DETONATING_BOMB);
        
        yield return new WaitForSeconds(1f);

        if (isFlippingNecessary)
        {
            Flip(impInventory.Explo.gameObject);
        }

        Collider2D[] objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
        foreach (Collider2D c in objectsWithinRadius)
        {
            if (c.gameObject.tag == "Obstacle")
            {
                Destroy(c.gameObject);
            }
        }

        StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
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
            case "Enemy":
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                InteractWith(enemy);
                break;
            case "Imp":
                ImpController imp = collision.gameObject.GetComponent<ImpController>();
                InteractWith(imp);
                break;
            case "Obstacle":
                ObstacleController obstacle = collision.gameObject.GetComponent<ObstacleController>();
                InteractWith(obstacle);
                break;
            case "Impassable":
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
            case "VerticalLadderSpot":
                if (type == ImpType.LadderCarrier)
                {
                    SetupVerticalLadder(collider.gameObject.transform.position); // TODO improve positioning
                }
                break;
            case "HorizontalLadderSpot":
                if (type == ImpType.LadderCarrier)
                {
                    SetupHorizontalLadder(collider.gameObject.transform.position); // TODO improve positioning
                }
                break;
            case "LadderBottom":
                ClimbLadder();
                break;
            case "LadderTop":
                movingUpwards = false;
                PlayWalkingAnimation();
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        /*string tag = collision.gameObject.tag;

        if (tag == "VerticalLadder" && movingUpwards == true)
        {
            movingUpwards = false;
            PlayWalkingAnimation();
        } */
    }

    private void PlayWalkingAnimation()
    {
        string anim;
        if (type == ImpType.Spearman)
        {
            anim = AnimationReferences.IMP_WALKING_SPEAR;
        }
        else
        {
            anim = AnimationReferences.IMP_WALKING_UNEMPLOYED;
        }
        animator.Play(anim);
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

    private void InteractWith(EnemyController enemy)
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

    IEnumerator TrainingRoutine(ImpType type)
    {

        float formerMovementSpeed = movementSpeed;

        impInventory.HideAllTools();
        animator.Play(AnimationReferences.IMP_TAKING_OBJECT);
        movementSpeed = 0f;

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

        animator.Play(AnimationReferences.IMP_WALKING_UNEMPLOYED);
    }

    private void TrainCoward()
    {
        impInventory.Display("Shield");
        animator.Play(AnimationReferences.IMP_HIDING_BEHIND_SHIELD);
    }

    private void TrainLadderCarrier(float formerMovementSpeed)
    {
        impInventory.Display("Ladder");
        StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
        animator.Play(AnimationReferences.IMP_WALKING_LADDER);
    }

    private void TrainSpearman(float formerMovementSpeed)
    {
        impInventory.Display("Spear");
        StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_WALKING);
        animator.Play(AnimationReferences.IMP_WALKING_SPEAR);
    }

    private void TrainBlaster(float formerMovementSpeed)
    {
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
        impInventory.Display("Bomb");
        StartMovingAgain(formerMovementSpeed, MOVEMENT_SPEED_RUNNING);
        animator.Play(AnimationReferences.IMP_WALKING_BOMB);
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

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
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
                if (collider.gameObject.tag == "Enemy")
                {
                    enemiesInAttackRange.Remove(collider.gameObject.GetComponent<EnemyController>());
                }
            }
        }
    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
    {
        if (self.GetInstanceID() == attackRange.GetInstanceID())
        {
            if (type == ImpType.Spearman)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    enemiesInAttackRange.Add(collider.gameObject.GetComponent<EnemyController>());
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
