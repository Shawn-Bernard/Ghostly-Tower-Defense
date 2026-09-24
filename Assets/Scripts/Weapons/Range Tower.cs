using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] private GameObject bullet;

    protected override void PerformAttack()
    {
        if (target == null || !isPlaced) return;

        if (!target.activeInHierarchy)
        {
            target = null;
        }
        else
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            Vector2 bulletDirection = GetDirection(target.transform.position, bullet.transform.position);

            bullet.transform.up = bulletDirection;

            bullet.SetActive(true);

        }
    }
}
