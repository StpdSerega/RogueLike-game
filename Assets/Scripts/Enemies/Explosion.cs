using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f); // Destroy the explosion effect after 1 second
    }
}
