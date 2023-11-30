using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 1500;
    public int goldValue = 20;
    public float currentHealth;

    private HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<HealthBar>();
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

        Destroy(gameObject);
    }
}
