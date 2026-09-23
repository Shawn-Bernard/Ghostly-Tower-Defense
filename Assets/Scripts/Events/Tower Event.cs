using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Event", menuName = "Events/Tower Event")]
public class TowerEvent : ScriptableObject
{
    public Action<Tower> registerEvent;
    public Action<Tower> unregisterEvent;

    public void RegisterTower(Tower tower)
    {
        registerEvent?.Invoke(tower);
    }
    public void UnregisterTower(Tower tower)
    {
        unregisterEvent?.Invoke(tower);
    }
}