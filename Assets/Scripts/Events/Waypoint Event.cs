using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Waypoint Event", menuName = "Events/Waypoint Event")]
public class WaypointEvent : ScriptableObject
{
    public Action<int, Action<Vector2>> gameEvent;

    /// <summary>
    /// Ghost will raise this event with their current index and will return a value/vector2
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector2 GetWaypoint(int index)
    {
        Vector2 waypoint = Vector2.zero;

        // Calling the event and sends index to waypoints and gets the value and stores in the return waypoint
        gameEvent?.Invoke(index, returnedValue =>
        {
            waypoint = returnedValue;
        });

        return waypoint;
    }
}
