using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    public GameObject enemyPrefab;
    public GameObject pickUpPrefab;

    [Header("Spawn Dimensions")]
    public float fixedYPosition = 0.5f; // Ensures everything is spawned at the same height 
    public float minXPosition = -5f;
    public float maxXPosition = 5f;
    public float minZPosition = -5f;
    public float maxZPosition = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the game with one PickUp
        SpawnPickUp(pickUpPrefab);
        
        // Start the Enemy spawning loop
        StartCoroutine(SpawnEnemy());
    }

    // (For Endless Mode) Spawn an instance of the PickUp object
    public void SpawnPickUp(GameObject prefab)
    {
        var spawnPosition = GetRandomSpawnPosition();
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    // (For Endless Mode) Spawn an Enemy at a set time
    IEnumerator SpawnEnemy()
    {
        // Create an infinite loop that continues spawning enemies as the game goes on
        while (true)
        {
            // Select a random spawn location and spawn the Enemy at the location
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
            // Wait after the designated seconds to spawn another Enemy
            yield return new WaitForSeconds(8);
        }
    }
    
    // (For Endless Mode) Calculate a random spawn position for the Enemy
    private Vector3 GetRandomSpawnPosition()
    {
        // Get a random X and Z value while keeping Y at a fixed position
        float randomX = Random.Range(minXPosition, maxXPosition);
        float randomZ = Random.Range(minZPosition, maxZPosition);
        return new Vector3(randomX, fixedYPosition, randomZ);
    }
}
