using System;
using UnityEngine;

[CreateAssetMenu(fileName = "String Event", menuName = "Events/String Event")]
public class StringEvent : ScriptableObject
{
    public Action<string> oneStringEvent;

    public Action<string, string> twoStringEvent;
    
    public void RaiseEvent(string newString) => oneStringEvent?.Invoke(newString);

    public void RaiseEvent(string newString,string newString2) => twoStringEvent?.Invoke(newString,newString2);
}