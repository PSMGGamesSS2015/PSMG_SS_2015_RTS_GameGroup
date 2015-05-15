using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The ImpController is a component attached to every instance of
/// an Imp prefab. It manages movement patterns and collision detection
/// of imps and listens for click events on the imps.
/// </summary>

public class ImpController : MonoBehaviour, TriggerCollider2D.TriggerCollider2DListener
{
   
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private float movementSpeed = 1f;
    private bool facingRight = true;
    private ImpControllerListener listener;
    private float attackCounter = 0f;
    private List<EnemyController> enemiesInAttackRange;

    private ImpType type;

    private ImpController commandPartner;

    private TriggerCollider2D impCollisionCheck;
    private TriggerCollider2D attackRange;

    public LayerMask impLayer;
    private bool isAtThrowingPosition;

    public interface ImpControllerListener
    {
        void OnImpSelected(ImpController impController);

        void OnImpHurt(ImpController impController);
    }
    
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemiesInAttackRange = new List<EnemyController>();
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

        isAtThrowingPosition = false;

        type = ImpType.Unemployed;
    }

    private void OnMouseDown()
    {
        listener.OnImpSelected(this);
    }

    public void RegisterListener(ImpControllerListener listener)
    {
        this.listener = listener;
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Update()
    {
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
    }

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

    public BoxCollider2D GetCollider()
    {
        return boxCollider2D;
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

    private void InteractWith(ObstacleController obstacle)
    {
        Debug.Log("Interacting with obstacle.");
    }

    private void InteractWith(EnemyController enemy)
    {
        Turn();
    }

    private void InteractWith(ImpController imp)
    {
        #region interaction logic for imps

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

        #endregion
    }

    private void ThrowImp(ImpController projectile)
    {
        // TODO 
    }

    private void FormCommand(ImpController commandPartner)
    {
        StopMoving();
        attackCounter = 0f;
        this.commandPartner = commandPartner;
    }

    private void DissolveCommand()
    {
        this.commandPartner = null;
    }

    private bool IsInCommand()
    {
        return commandPartner != null;
    }

    private void Turn()
    {
        movementSpeed *= -1;
        facingRight = !facingRight;
    }

    private void Move()
    {
        rigidBody2D.velocity = new Vector2(movementSpeed, rigidBody2D.velocity.y);
    }

    private void StartMoving()
    {
        if (facingRight)
        {
            movementSpeed = 1.0f;
        }
        else
        {
            movementSpeed = -1.0f;
        }
        
    }

    private void StopMoving()
    {
        movementSpeed = 0.0f;
    }

    public bool HasJob()
    {
        return type != ImpType.Unemployed;
    }

    public ImpType Type
    {
        get {
            return type;
        }
    }

    public void Train(ImpType type)
    {
        this.type = type;
        if (type == ImpType.Coward)
        {
            StopMoving();
        }
        else
        {
            StartMoving();
        }
    }

    public void Untrain()
    {
        type = ImpType.Unemployed;
        commandPartner = null;
    }

    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

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
        


    }

    void TriggerCollider2D.TriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
    {
        if (self.GetInstanceID() == attackRange.GetInstanceID())
        {
            if (collider.gameObject.tag == "Enemy")
            {
                enemiesInAttackRange.Add(collider.gameObject.GetComponent<EnemyController>());
            }
        }

    }

    public void LeaveGame()
    {
        listener.OnImpHurt(this);
        Destroy(gameObject);
    }

    public void UnregisterListener()
    {
        listener = null;
    }
}
