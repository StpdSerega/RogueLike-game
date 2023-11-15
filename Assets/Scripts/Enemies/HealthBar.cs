using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // �������� Slider �� ����� ����

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}
