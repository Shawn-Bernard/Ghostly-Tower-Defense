using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [SerializeField] float AttackRate;


    public GameObject bullet;
    [SerializeField] private bool canAttack;

    [SerializeField] UnityEvent attack;

    private List<GameObject> targets;
    private GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targets = new List<GameObject>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (targets != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);


            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);

            if (collision.gameObject == target)
            {
                target = targets.Count > 0 ? targets[0] : null;
            }
        }
    }

    IEnumerator Attack()
    {
        FindClosestTarget();

        canAttack = false;

        target.SetActive(false);

        float attackCooldown = 1 / AttackRate;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;


        if (target != null)
        {
            StartCoroutine(Attack());
        }
    }

    private void FindClosestTarget()
    {
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

        target = closestTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position,transform.position + transform.right * 2f);

        if (target != null)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }



}
