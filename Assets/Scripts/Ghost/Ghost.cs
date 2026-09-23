using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [Range(.01f, 0.5f)]
    [SerializeField] private float distanceWaypointCheck;

    [SerializeField] private int damageAmount;

    private Vector2 currentWaypoint;
    private int currentWaypointIndex;
    [SerializeField] private WaypointEvent waypointEvent;
    [SerializeField] private GhostEvent ghostEvent;
    [SerializeField] private GameState gameplayState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetWaypoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damageAmount);
                DestroySelf();
            }
        }
    }

    private void HandleMovement()
    {
        if (Vector2.Distance(transform.position, currentWaypoint) < distanceWaypointCheck)
        {
            SetWaypoint();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
        }
    }

    private void SetWaypoint()
    {
        currentWaypoint = waypointEvent.GetWaypoint(currentWaypointIndex);
        // Vector zero is the *null* return so reset or count up
        if (currentWaypoint != Vector2.zero)
        {
            currentWaypointIndex++;
        }
        else
        {
            // Reached the end of the path so maybe kill ghost or disable
            currentWaypointIndex = 0;
        }
        
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        gameplayState.onUpdateState += HandleMovement;
        ghostEvent.RegisterGhost(this);
    }

    private void OnDisable()
    {
        gameplayState.onUpdateState -= HandleMovement;
        ghostEvent.UnregisterGhost(this);
    }

}
