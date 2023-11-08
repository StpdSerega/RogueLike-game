using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints; // Array of spawn points
    public int maxEnemies = 10; // Maximum number of enemies in the room
    public float spawnDelay = 1f; // Delay before activating spawned enemies (in seconds)
    
    private int totalEnemiesSpawned = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (totalEnemiesSpawned < maxEnemies)
        {
            // Determine random enemy type to spawn
            GameObject enemyPrefab = GetRandomEnemyPrefab();

            // Determine random spawn point
            Transform spawnPoint = GetRandomSpawnPoint();

            // Spawn the enemy and deactivate it
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.SetActive(true);

            // Add the enemy to the list of active enemies
            activeEnemies.Add(enemy);

            totalEnemiesSpawned++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        // Logic to select random enemy prefab based on desired counts (e.g., 2 Light Enemies, 5 Medium Enemies, etc.)
        // Implement this logic based on your requirements
        // For now, return a random enemy from the provided array
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    Transform GetRandomSpawnPoint()
    {
        // Return a random spawn point from the array of spawn points
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    public void EnemyDied(GameObject enemy)
    {
        // Remove the dead enemy from the list of active enemies
        activeEnemies.Remove(enemy);

        // Check if all enemies are defeated
        if (activeEnemies.Count == 0 && totalEnemiesSpawned >= maxEnemies)
        {
            // Reward the player with a buff (implement your reward logic here)
            Debug.Log("All enemies defeated! Player rewarded with a buff.");
        }
    }
}
