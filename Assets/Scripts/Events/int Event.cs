using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Int Event", menuName = "Events/Int Event")]
public class intEvent : ScriptableObject
{
    public Action<int> gameEvent;

    public void RaiseEvent(int Int) => gameEvent?.Invoke(Int);
}
