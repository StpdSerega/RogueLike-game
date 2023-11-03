using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : MonoBehaviour
{
    public int attackBoost = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.IncreaseAttack(attackBoost);
                Destroy(gameObject);
            }
        }
    }
}
