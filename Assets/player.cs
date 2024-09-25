using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Player components
    public Rigidbody2D rb;

    // Movement variables
    private float xInput;
    public float moveSpeed = 5f;  // Speed of movement
    public float jumpForce = 10f; // Force applied when jumping
    public Animator anim;
    [SerializeField] private bool isMoving;
    private bool isGrounded;
    [SerializeField] private float groundCheckDistance = 0.1f;  // Set a default value
    [SerializeField] private LayerMask whatIsGround;

    private int facingDirection = 1;
    private bool facingRight = true;

    void Start()
    {
        // Initial setup
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CheckInput();  // Check for input

        // Check if grounded using raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        // Set animation parameters
        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;  // Adding a small threshold to avoid floating point issues
        anim.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.R))  // Flip manually if needed
            Flip();

        FlipController();  // Handle automatic flipping
    }

    private void CheckInput()
    {
        // Get horizontal input for movement
        xInput = Input.GetAxisRaw("Horizontal");
        
        // Apply horizontal velocity based on input
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        // Check for jumping input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Apply vertical jump force only if grounded
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Flip()
    {
        // Flip the player's facing direction
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);  // Rotate the player on the Y-axis to flip the sprite
    }

    private void FlipController()
    {
        // Automatically flip player based on movement direction
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check raycast in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
