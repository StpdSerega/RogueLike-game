using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : MonoBehaviour
{
    public float attackBoost = 20f;
    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUsed && other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.IncreaseAttack(attackBoost);
                isUsed = true; 
                Destroy(gameObject);
            }
        }
    }
}
