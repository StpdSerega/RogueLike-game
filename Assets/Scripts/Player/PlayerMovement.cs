using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isSquatting = false;
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5f;
    private bool isJumping = false;
    private bool isDashing = false;
    private float lastDashTime;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        lastDashTime = -dashCooldown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSquatting = true;
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 2f);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isSquatting = false;
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * 2f);
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time - lastDashTime > dashCooldown)
        {
            int dashDirection = (int)Mathf.Sign(horizontalInput);

            StartCoroutine(Dash(dashDirection));
            lastDashTime = Time.time;

            PlayerHealth.instance.isInvulnerable = true;
            StartCoroutine(DisableInvulnerability());
        }

        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }

        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        rb.rotation = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private IEnumerator Dash(int dashDirection)
    {
        isDashing = true;
        rb.velocity = new Vector2(dashDirection * dashForce, rb.velocity.y);
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        isDashing = false;

        PlayerHealth.instance.isInvulnerable = false;
    }

    private IEnumerator DisableInvulnerability()
    {
        yield return new WaitForSeconds(dashDuration);
        PlayerHealth.instance.isInvulnerable = false;
    }

    public void IncreaseSpeed(float percentage)
    {
        moveSpeed *= (1 + percentage / 100);
    }
}
