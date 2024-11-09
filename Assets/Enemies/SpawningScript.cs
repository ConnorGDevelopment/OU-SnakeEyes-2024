using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public GameObject enemy;           // The enemy prefab to spawn
    public BoxCollider[] spawnAreas;   // Array of BoxColliders defining the spawn areas
    public int numberOfEnemies = 5;    // Number of enemies to spawn
    public float spawnDelay = 0.5f;    // Delay between spawns
    public int destroyThreshhold = 15; // Threshold for destroyed enemies
    public float newWaveDelay = 10;    // Delay before a new wave spawns

    private List<GameObject> enemiesAlive = new List<GameObject>();
    private int destroyedEnemyCount = 0;
    private bool waveEnded = false;    // Tracks if the wave has ended
    private bool upgradeChosen = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = GetRandomPositionInRandomBox();
            GameObject newEnemy = Instantiate(enemy, randomPosition, Quaternion.identity);

            enemiesAlive.Add(newEnemy);
            newEnemy.GetComponent<EnemyDestruction>().OnDestroyed += OnEnemyDestroyed;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void OnEnemyDestroyed(GameObject enemy)
    {
        destroyedEnemyCount++;
        enemiesAlive.Remove(enemy);

        // Check if threshold is reached
        if (destroyedEnemyCount >= destroyThreshhold)
        {
            EndWave();
        }
    }

    void EndWave()
    {
        // Mark the wave as ended and clear the enemies
        waveEnded = true;
        destroyedEnemyCount = 0;

        // Destroy all remaining enemies
        foreach (GameObject enemy in enemiesAlive)
        {
            Destroy(enemy);
        }
        enemiesAlive.Clear();
    }

    public void OnUpgradePickedUp()
    {
        // Only start a new wave if the previous wave has ended
        if (waveEnded)
        {
            waveEnded = false;  // Reset wave-ended flag
            upgradeChosen = false;

            StartCoroutine(SpawnNewWaveAfterDelay());
        }
    }

    IEnumerator SpawnNewWaveAfterDelay()
    {
        yield return new WaitForSeconds(newWaveDelay);
        StartCoroutine(SpawnEnemies());
    }

    private Vector3 GetRandomPositionInRandomBox()
    {
        BoxCollider spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
