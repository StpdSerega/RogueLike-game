using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBuff : MonoBehaviour
{
    public int healthBoost= 1; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.IncreaseHealth(healthBoost);
                Destroy(gameObject); 
            }
        }
    }
}
