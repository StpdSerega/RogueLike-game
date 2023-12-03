using UnityEngine;

public class BossEnemyLocationFirst : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 25f;
    public float attackRange = 2.7f;
    public int attackDamage = 2;
    public float projectileSpeed = 5f;
    public int projectilesPerSide = 3;   
    private float damageAreaDuration = 1f; 

    public Rigidbody2D rb; 
    private int currentHealth;
    private bool isJumping = false;
    private float lastJumpTime;
    private float lastProjectileTime;

    public BossHealth bossHealth;
    public Transform leftFirePoint;
    public Transform rightFirePoint;

    public GameObject projectile;
    public GameObject damageAreaPrefab; 
    private GameObject damageAreaInstance;

    // «м≥нено на метод
    public bool IsJumping()
    {
        return isJumping;
    }

    void Start()
    {
        rb.freezeRotation = true;
        bossHealth = GetComponent<BossHealth>();
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRange)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            if (distanceToPlayer <= attackRange)
            {
                Attack(player);
            }
        }

        if (Time.time - lastJumpTime > (bossHealth.currentHealth > 1000 ? 12f : 7f) && !isJumping)
        {
            Jump();
        }

        if (bossHealth.currentHealth < 600 && Time.time - lastProjectileTime > 5f)
        {
            ShootProjectiles();
        }

        if (bossHealth.currentHealth < 1000 && bossHealth.currentHealth >= 600)
        {
            moveSpeed = 5f;
        }

        if (bossHealth.currentHealth < 600)
        {
            moveSpeed = 7f;
        }
    }

    void Attack(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;

        rb.AddForce(Vector2.up * 140f, ForceMode2D.Impulse);
        isJumping = true;
        lastJumpTime = Time.time;
    }

    void ShootProjectiles()
    {
        for (int i = 0; i < projectilesPerSide; i++)
        {
            Invoke("ShootSingleProjectile", i * 0.5f);
        }

        lastProjectileTime = Time.time;
    }

    void ShootSingleProjectile()
    {
        GameObject leftProjectile = Instantiate(projectile, leftFirePoint.position, Quaternion.identity);
        Rigidbody2D leftRb = leftProjectile.GetComponent<Rigidbody2D>();
        leftRb.velocity = Vector2.left * projectileSpeed;

        GameObject rightProjectile = Instantiate(projectile, rightFirePoint.position, Quaternion.identity);
        Rigidbody2D rightRb = rightProjectile.GetComponent<Rigidbody2D>();
        rightRb.velocity = Vector2.right * projectileSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
            ActivateDamageArea(); // јктивуЇмо область шкоди при приземленн≥
        }
    }

    void ActivateDamageArea()
    {
        float enemyHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        GameObject damageArea = Instantiate(damageAreaPrefab, transform.position - new Vector3(0f, enemyHeight / 2f, 0f), Quaternion.identity);

        DamageArea damageAreaScript = damageArea.GetComponent<DamageArea>();
        if (damageAreaScript != null)
        {
            damageAreaScript.StartDestroyTimer(1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}