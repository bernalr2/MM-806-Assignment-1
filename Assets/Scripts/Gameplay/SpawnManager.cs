using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    public GameObject enemyPrefab;
    public GameObject pickUpPrefab;

    [Header("Spawn Dimensions")]
    public float fixedSpawnHeight = 0.5f; // Fixed Y-axis spawn position
    public float minXPosition = -5f;
    public float maxXPosition = 5f;
    public float minZPosition = -5f;
    public float maxZPosition = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObject(pickUpPrefab);
    }

    public void SpawnObject(GameObject prefab)
    {
        var spawnPosition = GetRandomSpawnPosition();
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(minXPosition, maxXPosition);
        float randomZ = Random.Range(minZPosition, maxZPosition);
        return new Vector3(randomX, fixedSpawnHeight, randomZ);
    }
}
