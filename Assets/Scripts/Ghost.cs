using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [Range(.01f, 0.5f)]
    [SerializeField] private float distanceWaypointCheck;

    private Vector2 currentWaypoint;
    [SerializeField] private int currentWaypointIndex;
    [SerializeField] private WaypointEvent waypointEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
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

    private void Death()
    {
        gameObject.SetActive(false);
        currentWaypointIndex = 0;
    }
}
