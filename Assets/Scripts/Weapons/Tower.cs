using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ISelectable
{
    [SerializeField] private float attackCooldown;


    [SerializeField] protected bool canAttack;
    [SerializeField] protected bool isSelected;

    [SerializeField] private TowerEvent towerEvent;

    protected List<GameObject> targets;
    [SerializeField] protected GameObject target;

    private Vector2 oldPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (!target.activeInHierarchy || isSelected) target = null;


            float angle = GetAngle(target.transform.position, transform.position);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSelected) return;

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
        if (isSelected) return;

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

    public void Selected()
    {
        oldPosition = transform.position;
        canAttack = false;
        StopCoroutine(Attack());
        isSelected = true;
        target = null;
    }

    public void Unselected()
    {
        oldPosition = transform.position;
        isSelected = false;
        canAttack = true;
    }

    public void Cancelled()
    {
        transform.position = oldPosition;
    }

    protected float GetAngle(Vector2 targetPosition,Vector2 fromPosition)
    {
        float angleOffset = 90f;
        Vector2 direction = targetPosition - fromPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angleOffset;
        return angle;
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
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Vector3 left = Quaternion.Euler(0, 0, meleeAngle / 2f) * transform.up;
        Vector3 right = Quaternion.Euler(0, 0, -meleeAngle / 2f) * transform.up;

        Gizmos.DrawLine(
            transform.position,
            transform.position + left * meleeRange
        );

        Gizmos.DrawLine(
            transform.position,
            transform.position + right * meleeRange
        );
    }
    */

}
