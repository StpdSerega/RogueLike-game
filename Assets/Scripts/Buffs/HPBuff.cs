using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBuff : MonoBehaviour
{
    public int healthBoost = 1;
    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUsed && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(healthBoost);
                isUsed = true;
                Destroy(gameObject); 
            }
        }
    }
}
