using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovementAnimated : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 500f;
    public float fallThreshold = -0.1f;     // velocity considered "falling"
    public float landingGrace = 0.08f;      // small buffer after landing to avoid flicker

    Rigidbody2D rb;
    public bool isGrounded = false;         // maintained by trigger checks
    private bool shouldJump = false;

    Animator animator;
    SpriteRenderer spriteRenderer;

    // internal state
    private float lastLandTime = -10f;
    private bool computedFalling = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // read horizontal input here (non-physics)
        float horizontalInput = Input.GetAxis("Horizontal");

        bool movingHorizontally = Mathf.Abs(horizontalInput) > 0.01f;
        animator.SetBool("isRunning", movingHorizontally && isGrounded);

        // Flip sprite / running bool
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Jump input: set a flag to apply force in FixedUpdate (physics step)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            shouldJump = true;
        }

        // Optional: trigger jump animation here (it will be fired when we actually jump)
        // We do not set isGrounded=false here because the physics will update in FixedUpdate/OnLeftGround
    }

    void FixedUpdate()
    {
        // Horizontal movement using physics (better than Translate for consistent physics)
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 newVel = rb.linearVelocity;
        newVel.x = horizontalInput * moveSpeed;
        rb.linearVelocity = newVel;

        // Apply jump force in FixedUpdate if requested
        if (shouldJump)
        {
            shouldJump = false;
            rb.AddForce(Vector2.up * jumpForce);
            // set a trigger for the jump animation once
            if (animator != null) animator.SetTrigger("JumpTrigger");
            // mark left ground immediately to avoid being considered grounded next frame
            isGrounded = false;
        }

        // compute falling based on vertical velocity and grounded state
        computedFalling = (rb.linearVelocity.y < fallThreshold) && !isGrounded;
    }

    void LateUpdate()
    {
        // Landing grace: ignore brief negative-velocity frames immediately after landing
        bool justLanded = (Time.time - lastLandTime) <= landingGrace;
        bool animFalling = computedFalling && !justLanded;

        // Update animator parameters here (after physics & collisions)
        animator.SetBool("isFalling", animFalling);
        animator.SetBool("isGrounded", isGrounded);
    }

    // Ground detection - call OnLanded/OnLeftGround to maintain lastLandTime properly
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            OnLanded();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            OnLeftGround();
        }
    }

    // Call when we detect landing (from trigger/collision)
    public void OnLanded()
    {
        isGrounded = true;
        lastLandTime = Time.time;
    }

    // Call when leaving ground
    public void OnLeftGround()
    {
        isGrounded = false;
    }
}
