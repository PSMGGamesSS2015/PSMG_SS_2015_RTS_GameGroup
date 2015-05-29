using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

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
    private float movementSpeed = 0.6f;
    private bool facingRight = true;
    private bool movingUpwards = false;
    //profession-related
    private TriggerCollider2D attackRange;
    private float attackCounter = 0f;
    private List<EnemyController> enemiesInAttackRange;
    private ImpController commandPartner;
    private bool isAtThrowingPosition;
    private float bombCounter = 0f;
    //general
    private ImpType type;
    private ImpControllerListener listener;
    public LayerMask impLayer;
    //prefabs
    public GameObject verticalLadderPrefab;
    public GameObject horizontalLadderPrefab;
    private ImpInventory impInventory;
    // ui
    private bool areLabelsDisplayed;

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
        this.listener = listener;
    }

    public void UnregisterListener()
    {
        listener = null;
    }

    #endregion

    #region initialization, properties, input handling and update

    private void OnMouseDown()
    {
        listener.OnImpSelected(this);
    }

    private void OnGUI()
    {
        if (areLabelsDisplayed)
        {
            Handles.Label(new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.75f, 0), type.ToString());
        }
    }

    private void Awake()
    {
        InitComponents();
        InitAttributes();
        InitTriggerColliders();
        animator.Play("Imp Walking");
    }

    private void InitComponents()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        impInventory = GetComponentInChildren<ImpInventory>();
    }

    private void InitAttributes()
    {
        
        isAtThrowingPosition = false;
        type = ImpType.Unemployed;
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
            else
            {
                impCollisionCheck = c;
            }
        }
        impCollisionCheck.RegisterListener(this);
        attackRange.RegisterListener(this);

        enemiesInAttackRange = new List<EnemyController>();
    }

    public ImpType Type
    {
        get
        {
            return type;
        }
    }

    private void FixedUpdate()
    {
        
        
        if (type != ImpType.Coward && 
            !IsInCommand())
        {
            if (movingUpwards)
            {
                Debug.Log("Moving Upwards");
                MoveUpwards();
            }
            else
            {
                Move();
            }   
        }

        if (IsInCommand() && type == ImpType.Spearman)
        {
            if (attackCounter >= 4.0f)
            {
                attackCounter = 0f;
                Pierce();
            }
            else
            {
                attackCounter += Time.deltaTime;
            }
        }

        if (type == ImpType.Blaster)
        {
            bombCounter += Time.deltaTime;
            if (bombCounter >= 3.0f)
            {
                DetonateBomb();
            }
        }

    }

    public void LeaveGame()
    {
        listener.OnImpHurt(this);
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
        // flipping the gameobject
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }

    private void MoveUpwards()
    {
        rigidBody2D.velocity = new Vector2(0f, 1f); // TODO Stop moving upwards when reached top
    }

    private void ClimbLadder()
    {
        Debug.Log("Climbing ladder");
        movingUpwards = true;
        // check when the top is reached
    }

    #endregion

    #region profession-specific methods

    private void Pierce()
    {
        Debug.Log("Attacking");
        Debug.Log("Number of enemies in Attack range: " + enemiesInAttackRange.Count);
        foreach (EnemyController enemy in enemiesInAttackRange)
        {
            enemy.LeaveGame();
        }
        enemiesInAttackRange.Clear();
    }

    private void SetupVerticalLadder(Vector3 position)
    {
        Instantiate(verticalLadderPrefab, position, Quaternion.identity);
        Untrain();
    }

    private void SetupHorizontalLadder(Vector3 position)
    {
        Instantiate(horizontalLadderPrefab, position, Quaternion.Euler(0,0,-90));
        Untrain();
    }

    private void FormCommand(ImpController commandPartner)
    {
        attackCounter = 0f;
        if (type == ImpType.Spearman)
        {
            animator.Play("Imp Attacking with Spear");
        }
        this.commandPartner = commandPartner;
    }

    public void DissolveCommand()
    {
        if (type == ImpType.Spearman)
        {
            animator.Play("Imp Walking with Spear");
        }
        this.commandPartner = null;
    }

    public bool IsInCommand()
    {
        return commandPartner != null;
    }

    private void DetonateBomb()
    {
        Debug.Log("BOOOM");
        Collider2D[] objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
        foreach (Collider2D c in objectsWithinRadius)
        {
            if (c.gameObject.tag == "Obstacle")
            {
                Destroy(c.gameObject);
            }
        }
        Untrain();
        bombCounter = 0f;
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
                    Untrain();
                }
                break;
            case "HorizontalLadderSpot":
                if (type == ImpType.LadderCarrier)
                {
                    SetupHorizontalLadder(collider.gameObject.transform.position); // TODO improve positioning
                    Untrain();
                }
                break;
            case "VerticalLadder":
                ClimbLadder();
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == "VerticalLadder" && movingUpwards == true)
        {
            movingUpwards = false;
        }
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
        Debug.Log("Interacting with obstacle.");
        Turn();
    }

    private void InteractWith(EnemyController enemy)
    {
        Turn();
    }

    private void InteractWith(ImpController imp)
    {
        if (commandPartner == null &&
            ((type == ImpType.Coward && imp.Type == ImpType.Spearman) ||
            (type == ImpType.Spearman && imp.Type == ImpType.Coward)))
        {
            FormCommand(imp);
        }

         else if (imp.Type == ImpType.Coward &&
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

    // TODO refactor method
    IEnumerator TrainingRoutine(ImpType type)
    {
        float formerMovementSpeed = movementSpeed;

        impInventory.HideAllTools();
        animator.Play("Imp Taking Object");
        movementSpeed = 0f;
        yield return new WaitForSeconds(1.4f);

        this.type = type; // assign new type
        if (commandPartner != null)
        {
            commandPartner.DissolveCommand();
            DissolveCommand();
        }

        if (type == ImpType.Blaster)
        {
            bombCounter = 0f;
        }

        if (type == ImpType.Spearman)
        {
            impInventory.DisplaySpear();
            if (formerMovementSpeed < 0)
            {
                movementSpeed = -0.6f;
            }
            else
            {
                movementSpeed = 0.6f;
            }

            animator.Play("Imp Walking with Spear");
        }

        if (type == ImpType.LadderCarrier)
        {
            impInventory.DisplayLadder();
            if (formerMovementSpeed < 0)
            {
                movementSpeed = -0.6f;
            }
            else
            {
                movementSpeed = 0.6f;
            }
            animator.Play("Imp Walking with Ladder");
        }

        if (type == ImpType.Blaster)
        {
            impInventory.DisplayBomb();
            if (formerMovementSpeed < 0)
            {
                movementSpeed = -1.8f;
            }
            else
            {
                movementSpeed = 1.8f;
            }
            animator.Play("Imp Walking with Bomb");
        }

        if (type == ImpType.Unemployed)
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

            animator.Play("Imp Walking");
        }

        if (type == ImpType.Coward)
        {
            impInventory.DisplayShield();
            animator.Play("Imp Hiding Behind Shield");
        }

    }

    public bool HasJob()
    {
        return type != ImpType.Unemployed;
    }

    public void Untrain()
    {
        listener.OnUntrain(this);
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
