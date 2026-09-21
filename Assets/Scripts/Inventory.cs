using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private Weapon[] loadout;
    [SerializeField] private int maxTowerCount;
    [SerializeField] private TowerEvent towerEvent;
    [SerializeField] private WeaponEvent selectedWeaponEvent;
    [SerializeField] private List<Tower> activeTowers;

    [SerializeField] private float towerPlacementDistance;

    private void Awake()
    {
        activeTowers = new List<Tower>();
    }

    private void SelectSlot1()
    {
        SelectWeapon(0);
    }

    private void SelectSlot2()
    {
        SelectWeapon(1);
    }

    private void SelectSlot3()
    {
        SelectWeapon(2);
    }
    private void SelectSlot4()
    {
        SelectWeapon(3);
    }
    private void SelectSlot5()
    {
        SelectWeapon(4);
    }

    /// <summary>
    /// Select a weapon from loadout to place
    /// </summary>
    /// <param name="slotNumber"></param>
    private void SelectWeapon(int slotNumber)
    {
        if (loadout == null) return;

        Weapon selectableToPlace = Instantiate(loadout[slotNumber]);

        selectedWeaponEvent.RaiseEvent(selectableToPlace);
    }

    public bool IsTowerTooClose(Tower tower)
    {
        if (tower == null)
            return false;

        foreach (Tower activeTower in activeTowers)
        {
            if (activeTower == null || activeTower == tower)
                continue;

            if (Vector2.Distance(
                tower.transform.position,
                activeTower.transform.position) <= towerPlacementDistance)
            {
                return true;
            }
        }

        return false;
    }
    private void AddTower(Tower tower)
    {
        if (activeTowers.Count >= maxTowerCount)
        {
            tower.gameObject.SetActive(false);
        }
        activeTowers.Add(tower);
    }

    private void RemoveTower(Tower tower)
    {
        activeTowers.Remove(tower);
    }
    private void OnEnable()
    {
        inputActions.Slot1PerformedEvent += SelectSlot1;
        inputActions.Slot2PerformedEvent += SelectSlot2;
        inputActions.Slot3PerformedEvent += SelectSlot3;
        inputActions.Slot3PerformedEvent += SelectSlot4;
        inputActions.Slot3PerformedEvent += SelectSlot5;

        towerEvent.registerEvent += AddTower;
        towerEvent.unregisterEvent += RemoveTower;
    }

    private void OnDisable()
    {
        inputActions.Slot1PerformedEvent -= SelectSlot1;
        inputActions.Slot2PerformedEvent -= SelectSlot2;
        inputActions.Slot3PerformedEvent -= SelectSlot3;
        inputActions.Slot4PerformedEvent -= SelectSlot4;
        inputActions.Slot5PerformedEvent -= SelectSlot5;

        towerEvent.registerEvent -= AddTower;
        towerEvent.unregisterEvent -= RemoveTower;
    }
}
