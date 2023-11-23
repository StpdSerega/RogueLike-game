using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 25;
    public int goldValue = 2;
    private float currentHealth;

    private HealthBar healthBar; 

    void Start()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("EnemyHealthUI component not found!");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerGoldCounter playerGoldCounter = FindObjectOfType<PlayerGoldCounter>();
        if (playerGoldCounter != null)
        {
            playerGoldCounter.AddGold(goldValue);
        }

        EnemySpawnManager enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        if (enemySpawnManager != null)
        {
            enemySpawnManager.EnemyDied(gameObject);
        }

        Destroy(gameObject);
    }

}
