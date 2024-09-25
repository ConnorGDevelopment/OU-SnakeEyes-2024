using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemy;        // The enemy prefab to spawn
    public BoxCollider spawnArea;   // The BoxCollider defining the spawn area
    public int numberOfEnemies = 5; // Number of enemies to spawn

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = GetRandomPositionInBox();
            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionInBox()
    {
        // Get the size and center of the BoxCollider
        Vector3 boxSize = spawnArea.size;
        Vector3 boxCenter = spawnArea.transform.position + spawnArea.center;

        // Create random positions within the BoxCollider
        float randomX = Random.Range(boxCenter.x - boxSize.x / 2, boxCenter.x + boxSize.x / 2);
        float randomY = Random.Range(boxCenter.y - boxSize.y / 2, boxCenter.y + boxSize.y / 2);
        float randomZ = Random.Range(boxCenter.z - boxSize.z / 2, boxCenter.z + boxSize.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }
}
