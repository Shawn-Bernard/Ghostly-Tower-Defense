using UnityEngine;

public class Boomerang : Bullet
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float curveRange;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 middlePosition;
    private Vector3 rightPosition;
    private Vector3 leftPosition;

    private Vector3 currentTarget;

    private void OnEnable()
    {
        transform.SetParent(null);

        startPosition = transform.position;
        targetPosition = startPosition + transform.up * range;
        middlePosition = (startPosition + targetPosition) / 2;

        rightPosition = middlePosition + transform.right * curveRange;
        leftPosition = middlePosition + -transform.right * curveRange;

        currentTarget = targetPosition;
    }

    public override void HandleMovement()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            if (currentTarget == targetPosition)
            {
                currentTarget = startPosition;
            }
            else if (currentTarget == startPosition)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void HandleHit()
    {
        if (damageable == null) return;

        damageable.TakeDamage(damageAmount);
    }
    private void OnDrawGizmos()
    {
        // REMOVE THIS BEFORE TESTING PLEASE IT'LL CHANGE IN GAME TIME
        /*
        startPosition = transform.position;
        targetPosition = startPosition + transform.up * range;
        middlePosition = (startPosition + targetPosition) / 2;
        */
        Vector3 rightPosition = middlePosition + transform.right * curveRange;
        Vector3 leftPosition = middlePosition + -transform.right * curveRange;
        Gizmos.color = Color.yellow;

        
        Gizmos.DrawLine (startPosition, rightPosition);

        Gizmos.DrawLine(rightPosition, targetPosition);

        Gizmos.DrawLine(targetPosition, leftPosition);

        Gizmos.DrawLine(leftPosition, startPosition);
    }
}
