using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackDamage = 25;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.4f;
    public GameObject attackSpritePrefab; 

    private Vector2 lastMoveDirection = Vector2.right;
    private float lastAttackTime;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 directionToMouse = mousePosition - (Vector2)transform.position;
        directionToMouse.Normalize();

        float angle = Vector2.SignedAngle(Vector2.right, directionToMouse);
        lastMoveDirection = GetDirectionFromAngle(angle);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastAttackTime > attackCooldown)
        {
            PerformMeleeAttack();
            Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection * attackRange;
            ShowAttackSprite(attackPosition, lastMoveDirection); 
            lastAttackTime = Time.time;
        }
    }

    Vector2 GetDirectionFromAngle(float angle)
    {
        if (angle >= -22.5f && angle < 22.5f) // East
        {
            return Vector2.right;
        }
        else if (angle >= 22.5f && angle < 67.5f) // NorthEast
        {
            return new Vector2(1f, 1f).normalized;
        }
        else if (angle >= 67.5f && angle < 112.5f) // North
        {
            return Vector2.up;
        }
        else if (angle >= 112.5f && angle < 157.5f) // NorthWest
        {
            return new Vector2(-1f, 1f).normalized;
        }
        else if (angle >= 157.5f || angle < -157.5f) // West
        {
            return Vector2.left;
        }
        else if (angle >= -157.5f && angle < -112.5f) // SouthWest
        {
            return new Vector2(-1f, -1f).normalized;
        }
        else if (angle >= -112.5f && angle < -67.5f) // South
        {
            return Vector2.down;
        }
        else // SouthEast
        {
            return new Vector2(1f, -1f).normalized;
        }
    }

    void PerformMeleeAttack()
    {
        Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection * attackRange;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Boss"))
            {
                BossHealth bossHealth = enemy.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(attackDamage);
                }
            }
            else
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    void ShowAttackSprite(Vector2 attackPosition, Vector2 attackDirection)
    {
        GameObject attackSprite = Instantiate(attackSpritePrefab, attackPosition, Quaternion.identity);

        SpriteRenderer spriteRenderer = attackSprite.GetComponent<SpriteRenderer>();

        float angle = Vector2.SignedAngle(Vector2.right, attackDirection);
        spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(attackSprite, 0.3f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPosition = (Vector2)transform.position + lastMoveDirection * attackRange;
        Gizmos.DrawWireSphere(attackPosition, attackRange);
    }

    public void IncreaseAttack(float percentage)
    {
        attackDamage *= (1 + percentage / 100);
    }
}
