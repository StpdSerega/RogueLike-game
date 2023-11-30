using UnityEngine;

public class BossEnemyLocationFirst : MonoBehaviour
{
    public float moveSpeed = 3f; // Швидкість руху
    public float detectionRange = 25f; // Діапазон виявлення гравця
    public float attackRange = 2.7f; // Діапазон атаки
    public int attackDamage = 2; // Шкода атаки
    public Rigidbody2D rb; // Посилання на компонент Rigidbody2D боса
    public BossHealth bossHealth; // Посилання на компонент здоров'я боса

    public GameObject projectile; // Префаб снаряду
    public Transform leftFirePoint; // Точка стрільби зліва
    public Transform rightFirePoint; // Точка стрільби справа
    public float projectileSpeed = 5f; // Швидкість снаряду
    public int projectilesPerSide = 3; // Кількість снарядів з кожного боку

    private int currentHealth;
    private bool isJumping = false;
    private float lastJumpTime;
    private float lastProjectileTime;

    void Start()
    {
        rb.freezeRotation = true; // Заборона обертання для уникнення обертання
        bossHealth = GetComponent<BossHealth>(); // Отримуємо посилання на компонент здоров'я боса
    }

    void Update()
    {
        // Знаходимо всі об'єкти з тегом "Player" у сцені
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Проходимося по кожному об'єкту гравця
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Рухаємося в бік гравця, якщо він знаходиться в діапазоні виявлення
            if (distanceToPlayer <= detectionRange)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Заборона зміни Y-складової швидкості для уникнення літання
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y); // Зупинка руху, якщо гравець знаходиться за межами діапазону виявлення
            }

            // Атакуємо гравця, якщо він знаходиться в діапазоні атаки
            if (distanceToPlayer <= attackRange)
            {
                Attack(player);
            }
        }

        // Перевірка умови стрибка
        if (Time.time - lastJumpTime > (bossHealth.currentHealth > 1000 ? 12f : 6f) && !isJumping)
        {
            Jump();
        }

        // Перевірка умови стрільби снарядів
        if (bossHealth.currentHealth < 600 && Time.time - lastProjectileTime > 10f)
        {
            ShootProjectiles();
        }

        // Умова для збільшення швидкості, якщо здоров'я менше 700
        if (bossHealth.currentHealth < 1000 && bossHealth.currentHealth >= 600)
        {
            moveSpeed = 5f;
        }

        if (bossHealth.currentHealth < 600)
        {
            moveSpeed = 6f;
        }
    }

    void Attack(GameObject player)
    {
        // Логіка атаки (використовуйте скрипт EnemyHealth, якщо потрібно)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero; // Зупиняємо рух перед стрибком

        // Стрибаємо вгору
        rb.AddForce(Vector2.up * 200f, ForceMode2D.Impulse);
        isJumping = true;
        lastJumpTime = Time.time;
    }

    void ShootProjectiles()
    {
        // Стрільба снарядів
        for (int i = 0; i < projectilesPerSide; i++)
        {
            Invoke("ShootSingleProjectile", i * 0.5f); // Викликати стрільбу для кожного снаряду з затримкою
        }

        lastProjectileTime = Time.time; // Оновлюємо час останньої стрільби
    }

    void ShootSingleProjectile()
    {
        // Лівий снаряд
        GameObject leftProjectile = Instantiate(projectile, leftFirePoint.position, Quaternion.identity);
        Rigidbody2D leftRb = leftProjectile.GetComponent<Rigidbody2D>();
        leftRb.velocity = Vector2.left * projectileSpeed;

        // Правий снаряд
        GameObject rightProjectile = Instantiate(projectile, rightFirePoint.position, Quaternion.identity);
        Rigidbody2D rightRb = rightProjectile.GetComponent<Rigidbody2D>();
        rightRb.velocity = Vector2.right * projectileSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Перевіряємо зіткнення з гравцем
        if (collision.gameObject.CompareTag("Player"))
        {
            // Шкода для гравця при зіткненні з босом
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        // Визначаємо, чи бос знаходиться в стані стрибка
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Візуалізація діапазону виявлення та атаки
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
