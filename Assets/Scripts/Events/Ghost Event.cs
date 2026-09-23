using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost Event", menuName = "Events/Ghost Event")]
public class GhostEvent : ScriptableObject
{
    public Action<Ghost> registerEvent;
    public Action<Ghost> unregisterEvent;

    public void RegisterGhost(Ghost ghost) => registerEvent?.Invoke(ghost);
    public void UnregisterGhost(Ghost tower) => unregisterEvent?.Invoke(tower);
}
