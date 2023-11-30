using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    public float speedMultiplier = 13f;
    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUsed && other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.IncreaseSpeed(speedMultiplier);
                isUsed = true; 
                Destroy(gameObject);
            }
        }
    }
}
