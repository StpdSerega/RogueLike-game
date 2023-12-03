using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Text healthText;
    public bool isInvulnerable = false;

    private float invulnerabilityDuration = 2.0f;

    public static PlayerHealth instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            UpdateHealthText();
            isInvulnerable = true;
            StartCoroutine(DisableInvulnerability());

            if (currentHealth <= 0)
            {
                RestartGame();
            }

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 jumpForce = new Vector2(0f, 5f);
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("HomeScene");
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth;
    }

    private IEnumerator DisableInvulnerability()
    {
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    public void IncreaseHealth(int bonus)
    {
        currentHealth += bonus;
        UpdateHealthText();
    }

}
