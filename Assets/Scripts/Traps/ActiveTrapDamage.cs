using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrapDamage : MonoBehaviour
{
    private bool activated = true; 
    public int damage = 1; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                activated = false;

                Destroy(gameObject);
            }
        }
    }
}
