using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int goldValue = 2;
    private int currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

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
        // Implement death logic here (e.g., play death animation, drop items, etc.)
        Destroy(gameObject);
    }
}