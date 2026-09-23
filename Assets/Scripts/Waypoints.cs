using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private List<Vector2> waypoints;
    [SerializeField] private WaypointEvent waypointEvent;

    private void Awake()
    {
        AddWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        waypointEvent.gameEvent += GetWaypoint;
    }

    private void OnDisable()
    {
        waypointEvent.gameEvent -= GetWaypoint;
    }

    private void GetWaypoint(int index, Action<Vector2> returnWaypoint)
    {
        if (index < 0 || index >= waypoints.Count)
        {
            return;
        }

        returnWaypoint?.Invoke(waypoints[index]);
    }

    private void AddWaypoints()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Transform previousWaypoint = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform waypoint = transform.GetChild(i);

            if (!waypoint.CompareTag("Waypoint"))
                continue;

            if (previousWaypoint != null)
            {
                Gizmos.DrawLine(
                    previousWaypoint.position,
                    waypoint.position
                );
            }

            previousWaypoint = waypoint;
        }
    }
}
