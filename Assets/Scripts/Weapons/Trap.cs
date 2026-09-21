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
            gameObject.SetActive(false);
        }

    }
    public virtual void Cancelled()
    {

    }

    public virtual void Selected()
    {
    }

    public virtual void Unselected()
    {
    }
}
