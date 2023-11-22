using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float detectionRange = 10f; // Detection and attack range
    public float fireRange = 5f; // Range to start firing
    public float fireRate = 1f; // Firing rate in seconds
    public GameObject projectilePrefab; // Prefab of the projectile to fire
    public Transform firePoint; // Point where projectiles are spawned

    private bool isFiring = false;

    void Start()
    {
    }

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
                // Rotate the enemy to face the player
                Vector2 direction = (player.transform.position - transform.position).normalized;
                transform.up = direction;

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
            rb.velocity = firePoint.up * 10f; // Adjust the projectile speed as needed
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
}
