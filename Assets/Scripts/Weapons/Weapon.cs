using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, ISelectable
{
    [SerializeField] protected UnityEvent onSelectedWeapon;
    [SerializeField] protected UnityEvent onUnselectedWeapon;
    [SerializeField] protected UnityEvent onPickUp;
    [SerializeField] protected UnityEvent onPlaced;

    protected bool isPlaced;

    public virtual void Selected()
    {
        onSelectedWeapon?.Invoke();
    }

    public virtual void Unselected()
    {
        onUnselectedWeapon?.Invoke();
    }
    public void PickUp()
    {
        isPlaced = false;
        onPickUp?.Invoke();
    }

    public void Place()
    {
        isPlaced = true;
        onPlaced?.Invoke();
    }
}
