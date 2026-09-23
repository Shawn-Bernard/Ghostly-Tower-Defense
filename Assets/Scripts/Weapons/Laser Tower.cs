using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float range;

    protected override void PerformAttack()
    {
        if (target == null || !isPlaced)
            return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position,
            transform.up,
            range
        );

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable) && 
                hit.collider.CompareTag("Enemy"))// Just in case it hits the player base
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.up * range
        );
    }
}
