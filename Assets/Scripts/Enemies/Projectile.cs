using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionDelay = 1f;
    public float explosionRange = 2f;
    public GameObject explosionPrefab;

    void Start()
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, explosionRange, LayerMask.GetMask("Player"));
        foreach (Collider2D player in hitPlayers)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
