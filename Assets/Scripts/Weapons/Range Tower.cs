using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] private GameObject bullet;

    protected override void PerformAttack()
    {
        if (target == null || isSelected) return;

        if (!target.activeInHierarchy)
        {
            target = null;
        }
        else
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            float angle = GetAngle(target.transform.position, bullet.transform.position);

            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            bullet.SetActive(true);

        }
    }
}
