using System;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Weapon Event", menuName = "Events/Weapon Event")]
public class WeaponEvent : ScriptableObject
{
    public Action<Weapon> gameEvent;

    public void RaiseEvent(Weapon weapon) => gameEvent?.Invoke(weapon);
}

