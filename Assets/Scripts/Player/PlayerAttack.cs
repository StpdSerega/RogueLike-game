using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 1;
    public LayerMask enemyLayer;

    private Vector2 lastMoveDirection = Vector2.right; // Initial direction

    void Update()
    {
        // Detect player movement direction
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            lastMoveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PerformMeleeAttack();
        }
    }

    void PerformMeleeAttack()
    {
        // Calculate attack area based on the last movement direction
        Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection * attackRange;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection * attackRange;
        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }
}
