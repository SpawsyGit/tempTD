using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private float minSpawnCooldown = 10;
    [SerializeField] private float maxSpawnCooldown = 20;

    [SerializeField] private Vector2[] spawnPoints;

    [SerializeField] private int enemiesToSpawnBeforeBoss;
    private int enemiesSpawned;


    private float currentSpawnCooldown;
    private IEnumerator currentSpawnLoop;
    private bool spawnLoopIsRunning;

    private void Start()
    {
        SetNewSpawnCooldown();
        StartNewSpawnLoop();
    }

    public void StartNewSpawnLoop()
    {
        if (spawnLoopIsRunning) StopSpawnLoop();
        currentSpawnLoop = SpawnLoop();
        StartCoroutine(currentSpawnLoop);
    }
    public void StopSpawnLoop()
    {
        StopCoroutine(currentSpawnLoop);
        spawnLoopIsRunning = false;
        enemiesSpawned = 0;
    }

    private IEnumerator SpawnLoop()
    {
        spawnLoopIsRunning = true;
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnCooldown);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyToSpawn = enemiesSpawned >= enemiesToSpawnBeforeBoss ? bossPrefab : enemyPrefab;
        Instantiate(enemyToSpawn, spawnPoint, Quaternion.Euler(0, 0, 0));
        if (enemiesSpawned > enemiesToSpawnBeforeBoss) StopSpawnLoop();
        SetNewSpawnCooldown();
        enemiesSpawned++;
    }

    private void SetNewSpawnCooldown()
    {
        currentSpawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (Vector2 t in spawnPoints)
        {
            Gizmos.DrawSphere((Vector3)t, 0.1f);
        }
    }
}
