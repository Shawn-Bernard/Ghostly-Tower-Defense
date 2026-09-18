using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int damage;

    protected IDamageable damageable;

    private Transform originalParent;

    private void Awake()
    {
        originalParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            damageable = collision.GetComponent<IDamageable>();
            HandleHit();
        }
    }

    public virtual void HandleMovement()
    {
        transform.position = transform.position + transform.up * moveSpeed * Time.deltaTime;
    }

    public virtual void HandleHit()
    {
        if (damageable == null) return;

        damageable.TakeDamage(damage);

        gameObject.SetActive(false);
        transform.SetParent(originalParent);

        damageable = null;
    }

    private void OnEnable()
    {
        transform.SetParent(null);
    }

}
