using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private List<Vector2> waypoints;
    [SerializeField] private WaypointEvent waypointEvent;
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
        
    }
    /*
    private void SetWaypoint()
    {
        currentWaypoint = waypoints[currentWaypointIndex];

        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Count)
        {
            currentWaypointIndex = 0;
        }
    }*/
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
}
