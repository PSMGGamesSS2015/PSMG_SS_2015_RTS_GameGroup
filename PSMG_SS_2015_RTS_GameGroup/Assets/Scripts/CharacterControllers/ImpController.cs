using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The ImpController is a component attached to every instance of
/// an Imp prefab. It manages movement patterns and collision detection
/// of imps and listens for click events on the imps.
/// </summary>

public class ImpController : MonoBehaviour, ColliderHelper.ColliderHelperListener {
   
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private float raycastLength = 0.3f;
    private float movementSpeed = 1f;
    private ImpControllerListener listener;
    
    private ImpType impType;

    private ImpController commandPartner;

    private ColliderHelper colliderHelper;

    public LayerMask impLayer;
    private bool isAtThrowingPosition;

    public interface ImpControllerListener
    {
        void OnImpSelected(ImpController impController);
        void OnImpTrained(ImpController impController, ImpType impType);
    }
    
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        colliderHelper = GetComponentInChildren<ColliderHelper>(); 
        colliderHelper.RegisterListener(this);

        isAtThrowingPosition = false;

        impType = ImpType.Unemployed;
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
            ((GetType() == ImpType.Coward && imp.GetType() == ImpType.Spearman) ||
            (GetType() == ImpType.Spearman && imp.GetType() == ImpType.Coward)))
        {
            FormCommand(imp);
        }

         else if (imp.GetType() == ImpType.Coward &&
            (GetType() == ImpType.Unemployed    ||
            GetType() == ImpType.LadderCarrier  ||
            GetType() == ImpType.Blaster        ||
            GetType() == ImpType.Firebug        ||
            GetType() == ImpType.Botcher        ||
            GetType() == ImpType.Schwarzenegger))
        {
            Turn();
        }

        else if((GetType() == ImpType.Schwarzenegger) &&
            ((imp.GetType() != ImpType.Schwarzenegger) ||
             (imp.GetType() != ImpType.Coward)))
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
        // TODO
    }

    private void Turn()
    {
        movementSpeed *= -1;
        raycastLength *= -1;
    }

    private void Move()
    {
        rigidBody2D.velocity = new Vector2(movementSpeed, rigidBody2D.velocity.y);
    }

    private void StartMoving()
    {
        movementSpeed = 1.0f;
    }

    private void StopMoving()
    {
        movementSpeed = 0.0f;
    }

    public bool HasJob()
    {
        return impType != ImpType.Unemployed;
    }

    public ImpType GetType()
    {
        return impType;
    }

    public void Train(ImpType type)
    {
        this.impType = type;
        listener.OnImpTrained(this, type);
        if (type == ImpType.Coward)
        {
            StopMoving();
        }
    }

    public void Untrain()
    {
        impType = ImpType.Unemployed;
        commandPartner = null;
        listener.OnImpTrained(this, impType);
    }

    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    void ColliderHelper.ColliderHelperListener.OnTriggerExit2D(Collider2D collider)
    {
        ImpController imp = collider.gameObject.GetComponent<ImpController>();

        if (imp != null)
        {
            Physics2D.IgnoreCollision(GetCollider(), imp.GetCollider(), false);
        }
    }
}
