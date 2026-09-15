using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceWaypointCheck;

    private List<Vector2> waypoints;
    private Vector2 currentWaypoint;
    [SerializeField] private int currentWaypointIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = new List<Vector2>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Waypoint"))
            {
                waypoints.Add(transform.GetChild(i).position);
            }
        }
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
        currentWaypoint = waypoints[currentWaypointIndex];

        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Count)
        {
            currentWaypointIndex = 0;
        }
    }
}
