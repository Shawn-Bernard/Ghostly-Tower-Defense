using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float range;

    protected override void PerformAttack()
    {
        if (target == null || !isPlaced)
            return;

        Collider2D[] hits = Physics2D.OverlapAreaAll(
            transform.position,
            direction * range
        );
        
        foreach (var hit in hits)
        {
            if (hit == null) continue;

            if (hit.TryGetComponent<IDamageable>(out IDamageable damageable) && 
                hit.CompareTag("Enemy"))// Just in case it hits the player base
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        
    }

}
