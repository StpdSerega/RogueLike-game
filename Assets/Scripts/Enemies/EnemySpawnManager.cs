using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints; // Array of spawn points
    public int maxEnemies = 10; // Maximum number of enemies in the room
    public float spawnDelay = 1f; // Delay before activating spawned enemies (in seconds)
    public int enemiesNeededForNextStage = 5; // Number of enemies needed to transform the door
    public GameObject nextStageDoorPrefab; // Next stage door prefab

    private int totalEnemiesSpawned = 0;
    private int enemiesDefeated = 0;
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
        // Logic to select random enemy prefab based on desired counts
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    public void EnemyDied(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        enemiesDefeated++;

        if (activeEnemies.Count == 0 && totalEnemiesSpawned >= maxEnemies)
        {
            if (enemiesDefeated >= enemiesNeededForNextStage)
            {
                TransformDoorToNextStage();
            }
            else
            {
                Debug.Log("All enemies defeated! Player rewarded with a buff.");
            }
        }
    }

    void TransformDoorToNextStage()
    {
        // Find all doors in the scene using the tag
        GameObject[] enemySpawnDoors = GameObject.FindGameObjectsWithTag("EnemySpawnDoor");

        if (enemySpawnDoors.Length > 0)
        {
            // Choose a random door to transform
            GameObject randomDoor = enemySpawnDoors[Random.Range(0, enemySpawnDoors.Length)];

            // Instantiate the NextStageDoor prefab at the same position and rotation
            GameObject nextStageDoor = Instantiate(nextStageDoorPrefab, randomDoor.transform.position, randomDoor.transform.rotation);

            // Optionally, you can transfer any relevant information or state from the current door to the next stage door

            // Destroy the current door
            Destroy(randomDoor);
        }
        else
        {
            Debug.LogError("No EnemySpawnDoors found. Make sure they are tagged correctly.");
        }
    }

}
