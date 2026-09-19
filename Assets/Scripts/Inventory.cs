using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private Tower[] loadout;
    [SerializeField] private int maxTowerCount;
    [SerializeField] private TowerEvent towerEvent;
    [SerializeField] private List<Tower> activeTowers;

    private Tower selectedTower;

    private void Awake()
    {
        activeTowers = new List<Tower>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectSlot1()
    {
        SelectTower(0);
    }

    private void SelectSlot2()
    {
        SelectTower(1);
    }

    private void SelectSlot3()
    {
        SelectTower(2);
    }
    private void SelectSlot4()
    {
        SelectTower(3);
    }
    private void SelectSlot5()
    {
        SelectTower(4);
    }

    /// <summary>
    /// Select a tower from loadout to place
    /// </summary>
    /// <param name="slotNumber"></param>
    private void SelectTower(int slotNumber)
    {
        if (loadout == null) return;

        selectedTower = loadout[slotNumber];

        Debug.Log(selectedTower.name);
    }
    private void AddTower(Tower tower)
    {
        if (activeTowers.Count > maxTowerCount)
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
