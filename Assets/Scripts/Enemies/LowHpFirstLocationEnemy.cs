using System.Collections;
using UnityEngine;

public class LowHpFirstLocationEnemy : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float detectionRange = 8f; // Range to detect the player and start moving towards them
    public float attackRange = 3f; // Attack range
    public int attackDamage = 3; // Attack damage
    public Rigidbody2D rb; // Reference to the enemy's Rigidbody2D component
    public LayerMask playerLayer; // LayerMask for the player

    private int currentHealth;

    void Start()
    {
        rb.freezeRotation = true; // Lock rotation to prevent spinning
    }

    void Update()
    {
        // Find all GameObjects with the "Player" tag in the scene
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Iterate through each player object
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Move towards the player when within detection range
            if (distanceToPlayer <= detectionRange)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed); // Allow movement in Y direction for flying
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop moving if the player is out of detection range
            }

            // Attack the player when within attack range
            if (distanceToPlayer <= attackRange)
            {
                Attack(player);
            }
        }
    }

    void Attack(GameObject player)
    {
        // Handle attack logic (use EnemyHealth script if necessary)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Enemy is attacking player!");
        }
    }

    void FixedUpdate()
    {
        // Check for overlapping with the player and pass through
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 1f, playerLayer);
        if (playerCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, GetComponent<Collider2D>());
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
