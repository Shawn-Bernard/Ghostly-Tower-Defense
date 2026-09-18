using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerEvent towerEvent;

    [SerializeField] List<Tower> towers = new List<Tower>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddTower(Tower tower)
    {
        towers.Add(tower);
    }

    private void RemoveTower(Tower tower)
    {
        towers.Remove(tower);
    }

    private void OnEnable()
    {
        towerEvent.registerEvent += AddTower;
        towerEvent.unregisterEvent += RemoveTower;
    }
    private void OnDisable()
    {
        towerEvent.registerEvent -= AddTower;
        towerEvent.unregisterEvent -= RemoveTower;
    }
}
