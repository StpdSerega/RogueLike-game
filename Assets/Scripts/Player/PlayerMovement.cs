using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isSquatting = false; // Flag to track whether the player is squatting
    public float dashForce = 20f; // The force applied during dash
    public float dashDuration = 0.2f; // Duration of the dash in seconds
    public float dashCooldown = 5f; // Cooldown time for dash in seconds
    private bool isJumping = false;
    private bool isDashing = false;
    private float lastDashTime; // Variable to store the time when the last dash occurred
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider; // Reference to the BoxCollider2D component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        rb.freezeRotation = true;
        lastDashTime = -dashCooldown; // Set initial lastDashTime to ensure the player can dash immediately
    }

    void Update()
    {
        // Check for squat input (you can use any input you prefer, e.g., Input.GetKey(KeyCode.S))
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSquatting = true;
            // Reduce playerCollider size when squatting
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 2f);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isSquatting = false;
            // Reset playerCollider size when standing up
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * 2f);
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        // Dash input - use Left Shift key or any other input you prefer
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time - lastDashTime > dashCooldown)
        {
            // Determine the dash direction based on horizontal input
            int dashDirection = (int)Mathf.Sign(horizontalInput);

            StartCoroutine(Dash(dashDirection));
            lastDashTime = Time.time;
        }
        // Normal movement logic
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        rb.rotation = 0f; // Set rotation to 0 to prevent spinning
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with objects tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    // Coroutine for dash ability
    private IEnumerator Dash(int dashDirection)
    {
        isDashing = true;
        rb.velocity = new Vector2(dashDirection * dashForce, rb.velocity.y);
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
    }

}

