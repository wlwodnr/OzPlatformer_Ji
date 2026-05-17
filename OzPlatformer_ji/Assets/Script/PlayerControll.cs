using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float horizontalInput;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
            Debug.Log("A입력");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
            Debug.Log("D입력");

        }
        else
        {
            horizontalInput = 0f; 
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
        if (jumpRequested == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            jumpRequested = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}