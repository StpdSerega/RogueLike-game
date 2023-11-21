using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to your player prefab

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Spawn the player prefab at the spawner's position
            GameObject playerInstance = Instantiate(playerPrefab, transform.position, Quaternion.identity);

            // Optionally, you can set the player instance as a child of the spawner
            playerInstance.transform.parent = transform;
        }
        else
        {
            Debug.LogError("Player prefab reference is missing! Please assign the player prefab in the inspector.");
        }
    }
}
