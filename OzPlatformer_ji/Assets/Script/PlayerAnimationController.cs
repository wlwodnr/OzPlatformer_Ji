using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateMoveAnimation(float horizontalInput)
    {
        if (animator == null) return;

        animator.SetBool("IsRun", horizontalInput != 0f);

        if (horizontalInput < 0f && spriteRenderer != null)
        {
            spriteRenderer.flipX = true; 
        }
        else if (horizontalInput > 0f && spriteRenderer != null)
        {
            spriteRenderer.flipX = false; 
        }
    }

    public void SetJumpAnimation(bool isJumping)
    {
        if (animator != null)
        {
            animator.SetBool("IsJump", isJumping);
        }
    }
    public void ResetAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsRun", false);
            animator.SetBool("IsJump", false);
        }
    }
}