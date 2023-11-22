using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int goldValue = 2;
    private int currentHealth;

    private HealthBar healthBar; // ��������� �� EnemyHealthUI

    void Start()
    {
        currentHealth = maxHealth;

        // ����������� ����� ���������� ��� (���������, ����'���� EnemyHealthUI).
        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar == null)
        {
            Debug.LogError("EnemyHealthUI component not found!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // ��������� Slider ��� ������� �������� ������'�
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

        // Implement death logic here (e.g., play death animation, drop items, etc.)
        Destroy(gameObject);
    }

}
