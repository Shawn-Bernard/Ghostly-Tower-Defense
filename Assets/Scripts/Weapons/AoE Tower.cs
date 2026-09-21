using UnityEngine;

public class AoETower : Tower
{
    [SerializeField] private int damageAmount;
    protected override void PerformAttack()
    {
        for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
        {
            var targetToDamage = targets[targetIndex]; 

            DealDamage(target.GetComponent<IDamageable>());
        }
    }

    private void DealDamage(IDamageable target)
    {
        if (target == null) return;
        target.TakeDamage(damageAmount);
    }
}
