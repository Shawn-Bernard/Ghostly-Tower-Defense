using System;
using UnityEngine;

[CreateAssetMenu(fileName = "String Event", menuName = "Events/String Event")]
public class StringEvent : ScriptableObject
{
    public Action<string> gameEvent;
    
    public void RaiseEvent(string newString) => gameEvent?.Invoke(newString);
}