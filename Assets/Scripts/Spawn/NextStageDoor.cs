using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageDoor : MonoBehaviour
{
    public string nextSceneName = "NextScene"; // The name of the next scene
    public KeyCode interactKey = KeyCode.E; // The key to interact with the door

    private bool canInteract = false;

    void Update()
    {
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            // Trigger the logic for transitioning to the next stage
            TransitionToNextStage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            // Optionally, provide feedback to the player (e.g., display a prompt)
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            // Optionally, remove feedback to the player
        }
    }

    void TransitionToNextStage()
    {
        // Add any logic for transitioning to the next stage, such as loading the next scene
        // For example, you can use SceneManager.LoadScene
        SceneManager.LoadScene(nextSceneName);

        // For now, let's print a message to the console
        Debug.Log("Transitioning to the next stage");
    }
}