using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : Weapon
{
    [SerializeField] private Animator towerAnimator;
    [SerializeField] private float attackCooldown;


    protected bool canAttack;

    [SerializeField] private TowerEvent towerEvent;

    protected List<GameObject> targets;
    [SerializeField] protected GameObject target;
    [SerializeField] protected Vector2 direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (target != null)
        {
            if (!target.activeInHierarchy || !isPlaced) target = null;

            direction = (transform.position - target.transform.position).normalized;

            if (towerAnimator != null)
            {
                towerAnimator.SetFloat("x", direction.x);
                towerAnimator.SetFloat("y", direction.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlaced) return;

        if (collision.CompareTag("Enemy"))
        {
            if (!targets.Contains(collision.gameObject)) targets.Add(collision.gameObject);

            if (canAttack && targets.Count > 0)
            {
                StartCoroutine(Attack());
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isPlaced) return;

        if (collision.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);

            // If the target left area, find a new one
            if (collision.gameObject == target)
            {
                target = null;
                FindClosestTarget();
            }
        }
    }

    public override void Selected()
    {
        base.Selected();
        StopCoroutine(Attack());
        target = null;
    }

    public override void Unselected()
    {
        base.Unselected();
    }

    protected float GetAngle(Vector2 targetPosition,Vector2 fromPosition)
    {
        float angleOffset = 90f;
        Vector2 direction = GetDirection(targetPosition,fromPosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angleOffset;
        return angle;
    }
    protected Vector2 GetDirection(Vector2 targetPosition, Vector2 fromPosition)
    {
        Vector2 direction = (targetPosition - fromPosition).normalized;
        return direction;
    }

    protected virtual void PerformAttack()
    {
    }
    /// <summary>
    /// Starts shoot and waits for cooldown = attack rate and checks for new targets 
    /// </summary>
    /// <returns></returns>

    protected virtual IEnumerator Attack()
    {
        canAttack = false;
        
        FindClosestTarget();
        if (towerAnimator != null) towerAnimator.SetTrigger("isAttacking");
        PerformAttack();

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;

        if (targets.Count > 0)
        {
            StartCoroutine(Attack());
        }
    }
    /// <summary>
    /// This looks for the nearest enemy and makes them the new target
    /// </summary>
    private void FindClosestTarget()
    {
        if (targets == null || targets.Count == 0) return;

        
        targets.RemoveAll(target => !target.activeInHierarchy);

        GameObject closestTarget = null;

        foreach (GameObject target in targets)
        {
            if (closestTarget == null || 
                Vector2.Distance(transform.position,target.transform.position) 
                < Vector2.Distance(transform.position, closestTarget.transform.position)
                )
            {
                closestTarget = target;
            }
        }
        if (closestTarget == null)
        {
            target = null;
        }
        else
        {
            target = closestTarget;
        }
    }

    private void OnEnable()
    {
        towerEvent.RegisterTower(this);
    }

    private void OnDisable()
    {
        towerEvent.UnregisterTower(this);
        target = null;
        StopCoroutine(Attack());
    }

}
