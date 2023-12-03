using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float detectionRange = 10f; // Detection and attack range
    public float fireRange = 5f; // Range to start firing
    public float fireRate = 1f; // Firing rate in seconds
    public GameObject projectilePrefab; // Prefab of the projectile to fire
    public Transform firePoint; // Point where projectiles are spawned

    private bool isFiring = false;
    private bool isFacingRight = true; // Keep track of the enemy's facing direction

    void Update()
    {
        // Find all GameObjects with the "Player" tag in the scene
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Iterate through each player object
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Determine the direction to the player
                Vector2 direction = (player.transform.position - transform.position).normalized;

                // Face the player
                if (direction.x > 0 && !isFacingRight)
                {
                    Flip();
                }
                else if (direction.x < 0 && isFacingRight)
                {
                    Flip();
                }

                if (distanceToPlayer <= fireRange && !isFiring)
                {
                    // Start firing when the player is within the firing range
                    isFiring = true;
                    InvokeRepeating(nameof(FireProjectile), 0f, fireRate);
                }
                else if (distanceToPlayer > fireRange && isFiring)
                {
                    // Stop firing when the player moves out of the firing range
                    isFiring = false;
                    CancelInvoke(nameof(FireProjectile));
                }
            }
        }
    }

    void FireProjectile()
    {
        // Instantiate and fire a projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = (isFacingRight ? Vector2.right : Vector2.left) * 10f; // Adjust the projectile speed as needed
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isInvulnerable)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    // Flip the enemy to face the opposite direction
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}