using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHpFirstLocationEnemy : MonoBehaviour
{
    public float detectionRange = 10.0f;
    public Transform player;
    public float chargeTime = 1.0f; 
    public float attackSpeed = 5.0f; 
    public float reloadTime = 1.0f; 
    public int damage = 1;

    private bool isCharging = false;
    private bool isReloading = false;
    private Vector2 chargeDirection;

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && !isCharging && !isReloading)
        {
            StartCoroutine(ChargeAndAttack());
        }

        if (isCharging)
        {
            transform.Translate(chargeDirection * attackSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChargeAndAttack()
    {
        isCharging = true;

        chargeDirection = (player.position - transform.position).normalized;

        yield return new WaitForSeconds(chargeTime);

        isCharging = false;

        yield return new WaitForSeconds(0.5f);

        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isInvulnerable)
            {
                playerHealth.TakeDamage(damage);
                StartCoroutine(ReloadEnemy());
            }
        }

        if (other.CompareTag("Ground"))
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

            Vector2 previousPosition = currentPosition - chargeDirection * 0.25f; 

            transform.position = new Vector3(previousPosition.x, previousPosition.y, transform.position.z);

        }
    }

    IEnumerator ReloadEnemy()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}
