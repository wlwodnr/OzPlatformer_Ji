using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;

    private PlayerAnimationController playerAnim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float horizontalInput;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnim = GetComponent<PlayerAnimationController>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive)
        {
            horizontalInput = 0f;
            jumpRequested = false;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            if (playerAnim != null) playerAnim.ResetAnimation();
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }
        else
        {
            horizontalInput = 0f;
        }

        if (playerAnim != null)
        {
            playerAnim.UpdateMoveAnimation(horizontalInput);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    public void Jump()
    {
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;

            if (playerAnim != null) playerAnim.SetJumpAnimation(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (playerAnim != null) playerAnim.SetJumpAnimation(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            if (playerAnim != null) playerAnim.SetJumpAnimation(true);
        }
    }
}