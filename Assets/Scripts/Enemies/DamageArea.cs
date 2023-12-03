using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damageAmount = 1;

    void Start()
    {
        // �������� ������ �������� ����� 1 �������
        StartDestroyTimer(1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �������� �� �������� �������� �������
        if (other.CompareTag("Player"))
        {
            // �������� ����� �������
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    public void StartDestroyTimer(float time)
    {
        // ��������� ������ ��������
        Destroy(gameObject, time);
    }
}
