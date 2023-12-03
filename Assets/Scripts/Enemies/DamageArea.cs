using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damageAmount = 1;

    void Start()
    {
        // Активуємо таймер знищення через 1 секунду
        StartDestroyTimer(1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Перевірка чи колайдер належить гравцеві
        if (other.CompareTag("Player"))
        {
            // Наносимо шкоду гравцеві
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    public void StartDestroyTimer(float time)
    {
        // Запускаємо таймер знищення
        Destroy(gameObject, time);
    }
}
