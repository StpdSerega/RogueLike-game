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
                Vector2 jumpForce = new Vector2(0f, 10f);
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
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
}
