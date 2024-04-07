using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject enemyPrefab2;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float waveTime = 10f;
    [SerializeField] int startingEnemyCount = 2;
    [SerializeField] float timeBetweenEnemies = 1f;

    private int currentWave = 1;
    private int enemiesSpawned = 0;

    public bool startWaves = false;

    void Update()
    {
        if (startWaves && !isSpawning)
        {
            StartCoroutine(SpawnWave());
        }
    }

    private bool isSpawning = false;

    IEnumerator SpawnWave()
    {
        isSpawning = true;

        while (true)
        {
            yield return new WaitForSeconds(waveTime);

            startingEnemyCount++;
            enemiesSpawned = 0;

            GameObject enemyPrefabToSpawn = (currentWave >= 3) ? enemyPrefab2 : enemyPrefab1;

            while (enemiesSpawned < startingEnemyCount)
            {
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefabToSpawn, spawnPoints[randomSpawnIndex].position, Quaternion.identity);
                enemiesSpawned++;
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            currentWave++;
        }

        isSpawning = false;
    }
}
