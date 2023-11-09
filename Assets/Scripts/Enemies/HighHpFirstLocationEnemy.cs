using UnityEngine;

public class HighHpFirstLocationEnemy : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed
    public float detectionRange = 5f; // Range to detect the player and start moving towards them
    public float attackRange = 1f; // Close-range attack range
    public int attackDamage = 2; // Attack damage
    public Transform player; // Reference to the player's Transform
    public Rigidbody2D rb; // Reference to the enemy's Rigidbody2D component

    private int currentHealth;

    void Start()
    {
        rb.freezeRotation = true; // Lock rotation to prevent spinning
    }

    void Update()
    {
        if (player == null)
        {
            // Handle the case when the player is destroyed or not assigned yet
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Move towards the player when within detection range
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Lock Y velocity to prevent flying
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y); // Stop moving if the player is out of detection range
        }

        // Attack the player when within attack range
        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Handle attack logic (use EnemyHealth script if necessary)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Enemy is attacking player!");
        }
    }


    void OnDrawGizmosSelected()
    {
        // Draw detection range gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw attack range gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
