using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [SerializeField] float attackRate;


    [SerializeField] GameObject bullet;
    [SerializeField] private bool canAttack;

    //[SerializeField] UnityEvent attack;

    [SerializeField] private List<GameObject> targets;
    private GameObject target;

    private void Awake()
    {
        canAttack = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targets != null)
        {

            
        }
        if (canAttack && targets.Count > 0)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
            FindClosestTarget();
            if (canAttack && targets.Count > 0)
            {
                Shoot();
                StartCoroutine(Attack());
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);
        }
    }

    private float GetAngle(Vector2 targetPosition,Vector2 currentPosition )
    {
        Vector2 direction = targetPosition - currentPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }

    private void Shoot()
    {
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        Vector2 direction = target.transform.position - bullet.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        bullet.SetActive(true);
    }

    IEnumerator Attack()
    {
        FindClosestTarget();
        //Shoot();

        canAttack = false;
        

        float attackCooldown = 1 / attackRate;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;


        if (target != null)
        {
            //StartCoroutine(Attack());
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
