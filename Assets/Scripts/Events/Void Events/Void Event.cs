using System;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Void Event", menuName = "Events/Void Event")]
public class VoidEvent : ScriptableObject
{
    public Action gameEvent;

    public void RaiseEvent() => gameEvent?.Invoke();
}
