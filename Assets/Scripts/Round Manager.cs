using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private float spawnInterval; // Wait time for each enemy
    [SerializeField] private int enemiesPerWave; // How many enemies spawn per wave in a round
    [SerializeField] private int totalWave; // How many waves spawn per round
    [SerializeField] private float waveInterval; // Wait time per wave in a round

    [SerializeField] private int currentRound;

    [SerializeField] private WaypointEvent waypointEvent;
    [SerializeField] private Vector2 spawnPoint;

    [SerializeField] private List<Ghost> activeGhosts = new List<Ghost>();
    [SerializeField] private List<Ghost> ghostTypes = new List<Ghost>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = waypointEvent.GetWaypoint(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            StartRound();
        }
    }
    private IEnumerator SpawnWaves()
    {
        currentRound++;

        for (int currentWave = 0; currentWave < totalWave; currentWave++)
        {
            for (int enemyCount = 0; enemyCount < enemiesPerWave; enemyCount++)
            {
                SpawnGhost();

                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(waveInterval);
        }
        
    }

    private void SpawnGhost()
    {
        Ghost ghost = ghostTypes[0];

        Instantiate(ghost, (Vector3)spawnPoint,Quaternion.identity);
        ghost.gameObject.SetActive(true);
    }

    private void StartRound()
    {
        currentRound++;
        StartCoroutine(SpawnWaves());
    }


}
