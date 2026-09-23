using UnityEngine;

public class Trap : Weapon
{
    [SerializeField] private int charges;

    [SerializeField] private int damageAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlaced) return;
        if (charges == 0) return;

        if (collision.CompareTag("Enemy"))
        {
            DealDamage(collision.GetComponent<IDamageable>());
        }
    }

    private void DealDamage(IDamageable target)
    {
        if (target == null) return;
        target.TakeDamage(damageAmount);
        charges--;

        if (charges == 0)
        {
            Destroy(gameObject);
        }

    }
}
