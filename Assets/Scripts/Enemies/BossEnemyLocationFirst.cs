using UnityEngine;

public class BossEnemyLocationFirst : MonoBehaviour
{
    public float maxHealth = 1000f;
    public float attackDamage = 1f;
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public float jumpCooldown = 20f;
    public float chaseRangeStage1 = 10f;
    public float chaseRangeStage2 = 15f;
    public float chaseRangeStage3 = 20f;
    public float jumpHeightStage1 = 5f;
    public float jumpHeightStage2 = 8f;
    public float jumpHeightStage3 = 10f;
    public float jumpFallMultiplier = 2f;
    public float projectileCooldownStage3 = 10f;
    public float attackRange = 1f; // Додано attackRange
    public Rigidbody2D rb; // Reference to the enemy's Rigidbody2D component
    public GameObject projectilePrefab;

    private float currentHealth;
    private bool isJumping = false;
    private float lastJumpTime;
    private float lastProjectileTime;


    private enum BossStage
    {
        Stage1,
        Stage2,
        Stage3
    }

    private BossStage currentStage;

    void Start()
    {
        rb.freezeRotation = true; // Lock rotation to prevent spinning
        currentHealth = maxHealth; // Змінено ініціалізацію на maxHealth
        currentStage = BossStage.Stage1;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            UpdateBossStage(distanceToPlayer);

            switch (currentStage)
            {
                case BossStage.Stage1:
                    MoveAndAttack(player, chaseRangeStage1);
                    Jump(jumpHeightStage1, jumpCooldown);
                    break;
                case BossStage.Stage2:
                    MoveAndAttack(player, chaseRangeStage2);
                    Jump(jumpHeightStage2, jumpCooldown);
                    break;
                case BossStage.Stage3:
                    MoveAndAttack(player, chaseRangeStage3);
                    Jump(jumpHeightStage3, jumpCooldown);
                    ShootProjectiles(projectileCooldownStage3);
                    break;
            }
        }
    }

    void UpdateBossStage(float distanceToPlayer)
    {
        if (currentHealth > 700)
        {
            currentStage = BossStage.Stage1;
        }
        else if (currentHealth > 350)
        {
            currentStage = BossStage.Stage2;
        }
        else
        {
            currentStage = BossStage.Stage3;
        }
    }

    void MoveAndAttack(GameObject player, float chaseRange)
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= chaseRange)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            if (distanceToPlayer <= attackRange)
            {
                Attack(player);
            }
        }
    }

    void Jump(float jumpHeight, float jumpCooldown)
    {
        if (!isJumping && Time.time - lastJumpTime > jumpCooldown)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Mathf.Sqrt(jumpHeight * -2f * Physics2D.gravity.y), ForceMode2D.Impulse);
            isJumping = true;
            lastJumpTime = Time.time;
        }

        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultiplier - 1) * Time.deltaTime;
        }
    }

    void ShootProjectiles(float projectileCooldown)
    {
        if (Time.time - lastProjectileTime > projectileCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Destroy(projectile, 3f);
            lastProjectileTime = Time.time;
        }
    }

    void Attack(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(Mathf.RoundToInt(attackDamage));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Дії, які відбуваються при зіткненні з гравцем (за вашим вибором)
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRangeStage1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRangeStage2);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRangeStage3);
    }
}