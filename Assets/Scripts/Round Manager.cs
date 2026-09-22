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
    private bool hasRoundStarted;

    [SerializeField] private WaypointEvent waypointEvent;
    [SerializeField] private Vector2 spawnPoint;

    [SerializeField] private List<Ghost> activeGhosts = new List<Ghost>();
    [SerializeField] private List<Ghost> ghostTypes = new List<Ghost>();

    [SerializeField] private VoidEvent startRoundEvent;
    [SerializeField] private GhostEvent ghostEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = waypointEvent.GetWaypoint(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator SpawnWaves()
    {
        hasRoundStarted = true;
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

        hasRoundStarted = false;

    }

    private void SpawnGhost()
    {
        Ghost ghost = ghostTypes[0];

        Instantiate(ghost, (Vector3)spawnPoint,Quaternion.identity);
        ghost.gameObject.SetActive(true);
    }

    private void StartRound()
    {
        if (CanStartRound())
        {
            currentRound++;
            StartCoroutine(SpawnWaves());
        }
    }

    private bool CanStartRound()
    {
        if (activeGhosts.Count <= 0 && !hasRoundStarted)
        {
            return true;
        }

        return false;
    }

    private void RemoveGhost(Ghost ghost)
    {
        if (activeGhosts != null)
        {
            activeGhosts.Remove(ghost);
        }
    }

    private void AddGhost(Ghost ghost)
    {
        if (activeGhosts != null)
        {
            activeGhosts.Add(ghost);
        }
    }

    private void OnEnable()
    {
        startRoundEvent.gameEvent += StartRound;
        ghostEvent.unregisterEvent += RemoveGhost;
        ghostEvent.registerEvent += AddGhost;
    }

    private void OnDisable()
    {
        startRoundEvent.gameEvent -= StartRound;
        ghostEvent.unregisterEvent -= RemoveGhost;
        ghostEvent.registerEvent -= AddGhost;
    }


}
