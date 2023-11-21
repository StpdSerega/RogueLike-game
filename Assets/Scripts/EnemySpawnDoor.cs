using System.Collections;
using UnityEngine;

public class EnemySpawnDoor : MonoBehaviour
{
    public GameObject nextStageDoorPrefab; // Next stage door prefab
    public float transformDelay = 2f; // Delay before transforming the door (in seconds)

    private bool isTransformed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransformed)
        {
            StartCoroutine(TransformToNextStageDoor());
        }
    }

    IEnumerator TransformToNextStageDoor()
    {
        // Add any transition effects or animations here
        Debug.Log("Player entered the enemy spawn door!");

        // Wait for the specified delay before transforming the door
        yield return new WaitForSeconds(transformDelay);

        // Transform the door into a NextStageDoor
        TransformToNextStageDoorLogic();

        // Set the flag to avoid repeated transformations
        isTransformed = true;
    }

    void TransformToNextStageDoorLogic()
    {
        // Instantiate the NextStageDoor prefab at the same position and rotation
        GameObject nextStageDoor = Instantiate(nextStageDoorPrefab, transform.position, transform.rotation);

        // Optionally, you can transfer any relevant information or state from the current door to the next stage door

        // Destroy the current door
        Destroy(gameObject);
    }
}
