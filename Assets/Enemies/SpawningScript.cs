using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public GameObject enemy;           // The enemy prefab to spawn
    public BoxCollider[] spawnAreas;   // Array of BoxColliders defining the spawn areas
    public int numberOfEnemies = 5;    // Number of enemies to spawn
    public float spawnDelay = 0.5f;      //spawning enemies over time
    public int destroyThreshhold = 15;   //Looking at how many enemies need to be destroyed in order to start a new wave
    public float newWaveDelay = 10;     //The delay before a new wave spawns, will change later once upgrades are in place

    private List<GameObject> enemiesAlive = new List<GameObject>();
    private int destroyedEnemyCount = 0;



    // Start is called before the first frame update
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

            
            enemiesAlive.Add(newEnemy);   //Adding the spawned enemy to the alive list

            newEnemy.GetComponent<EnemyDestruction>().OnDestroyed += OnEnemyDestroyed;  //Linking the enemies destruction to the counting method
            
            yield return new WaitForSeconds(spawnDelay);
           
        }
    }

    IEnumerator ResetEnemies()
    {
        //Destroy all remaining enemies
        foreach (GameObject enemy in enemiesAlive)
        {
            Destroy(enemy);
        }


        enemiesAlive.Clear();
        destroyedEnemyCount = 0; //Reset the destroyed enemy count

        //Wait a bit before respawning
        yield return new WaitForSeconds(newWaveDelay);

        StartCoroutine(SpawnEnemies()); //Spawning enemies again :)

    }

    void OnEnemyDestroyed(GameObject enemy)
    {
        destroyedEnemyCount++;
        enemiesAlive.Remove(enemy);   //Remove the enemy from the list

        if (destroyedEnemyCount >= destroyThreshhold)
        {
            StartCoroutine(ResetEnemies());
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
