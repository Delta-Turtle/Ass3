using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5f;              // Movement speed
    public Animator animator;                  // Reference to Animator
    private Vector2 moveInput;                // Stores movement input

    [HideInInspector] public bool isHunting = false;
    [HideInInspector] public bool isDead = false;

    private void Update()
    {
        // If dead, stop movement
        if (isDead)
        {
            moveInput = Vector2.zero;
            UpdateAnimator();
            return;
        }

        // Get input from arrow keys or WASD
        moveInput.x = Input.GetAxisRaw("Horizontal"); // -1 = left, 1 = right
        moveInput.y = Input.GetAxisRaw("Vertical");   // -1 = down, 1 = up

        // Optional: prevent diagonal movement (if you want)
        if (moveInput.x != 0) moveInput.y = 0;

        // Move PacStudent
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        // Update Animator parameters
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetBool("IsHunting", isHunting);
        animator.SetBool("IsDead", isDead);
    }

    // Call this method when PacStudent eats a power pellet
    public void StartHunt()
    {
        isHunting = true;
        // Optional: stop hunting after a delay
        Invoke(nameof(EndHunt), 10f); // Hunt lasts 10 seconds
    }

    private void EndHunt()
    {
        isHunting = false;
    }

    // Call this method when PacStudent dies
    public void Die()
    {
        isDead = true;
    }
}
