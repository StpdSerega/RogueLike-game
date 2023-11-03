using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveTrap : MonoBehaviour
{
    public int damage = 1; 

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isInvulnerable)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
