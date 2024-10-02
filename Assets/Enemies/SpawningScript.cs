using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public GameObject enemy;           // The enemy prefab to spawn
    public BoxCollider[] spawnAreas;   // Array of BoxColliders defining the spawn areas
    public int numberOfEnemies = 5;    // Number of enemies to spawn

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = GetRandomPositionInRandomBox();
            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionInRandomBox()
    {
        // Pick a random BoxCollider from the array
        BoxCollider spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];

        // Get the bounds of the BoxCollider in world space
        Bounds bounds = spawnArea.bounds;

        // Generate random positions within the bounds of the BoxCollider
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
