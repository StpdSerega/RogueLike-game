using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int goldValue = 2;
    private int currentHealth;

    private HealthBar healthBar; // Посилання на EnemyHealthUI

    void Start()
    {
        currentHealth = maxHealth;

        // Знаходження інших компонентів тут (наприклад, прив'язка EnemyHealthUI).
        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("EnemyHealthUI component not found!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Оновлення Slider при кожному зніманні здоров'я
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

        // Реалізуйте тут логіку смерті (наприклад, відтворення анімації смерті, викидання предметів і т.д.).
        Destroy(gameObject);
    }
}
