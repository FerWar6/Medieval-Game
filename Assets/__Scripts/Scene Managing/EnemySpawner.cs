using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject enemyPrefab2;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform middle;

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

        // Spawn the first wave instantly
        yield return StartCoroutine(SpawnEnemies());

        while (true)
        {
            yield return new WaitForSeconds(waveTime);

            startingEnemyCount++;
            enemiesSpawned = 0;

            GameObject enemyPrefabToSpawn = (currentWave >= 3) ? enemyPrefab2 : enemyPrefab1;
            yield return StartCoroutine(SpawnEnemies(enemyPrefabToSpawn));

            currentWave++;
        }

        isSpawning = false;
    }

    private IEnumerator SpawnEnemies(GameObject prefab = null)
    {
        int numEnemiesToSpawn = (prefab != null) ? startingEnemyCount : 1;
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemyToSpawn = (prefab != null) ? prefab : enemyPrefab1;
            Instantiate(enemyToSpawn, spawnPoints[randomSpawnIndex].position, Quaternion.identity);
            EnemyBehaviour enemyBehaviour = enemyToSpawn.GetComponent<EnemyBehaviour>();
            enemyBehaviour.SetWalkPoint(middle.position);
            enemiesSpawned++;
            if (prefab != null)
            {
                // Wait between spawning each enemy if it's not the first wave
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
        }
    }
}
