using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this to access UI components

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

    // Crouch variables
    private bool isCrouching = false;
    private CapsuleCollider2D collider; // Reference to the player's collider
    public float crouchHeight = 0.5f; // Height when crouched
    private float originalHeight; // Original height of the collider

    private int facingDirection = 1;
    private bool facingRight = true;

    // Health and score variables
    public int maxHealth = 100;    // Maximum health
    private int currentHealth;     // Current health of the player
    public int score = 0;          // Player's score

    // Reference to the HealthBar script
    public HealthBar healthBar;

    // ** New Scoreboard Reference **
    public TMPro.TextMeshProUGUI scoreText;


    void Start()
    {
        // Initial setup
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        collider = GetComponent<CapsuleCollider2D>(); // Get the collider component
        originalHeight = collider.size.y; // Store the original height

        // Initialize health
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);  // Set health bar to max health

        // Initialize score display
        UpdateScoreText();
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

        // Handle crouching
        HandleCrouch();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar with current health
        healthBar.SetHealth(currentHealth);

        // Check if health is 0 or below, if so, trigger death
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void IncreaseScore(int amount)
{
    score += amount; // Increase the score by the given amount

    // Assuming you have a Text object to display the score
    if (scoreText != null)
    {
        scoreText.text = "Score: " + score.ToString();  // Update the score on the UI
    }
}


    // ** Function to update the score **
    public void AddScore(int points)
    {
        score += points;  // Add points to the score
        UpdateScoreText();  // Update the score UI
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;  // Update the UI text
    }

    private void CheckInput()
    {
        // Get horizontal input for movement
        xInput = Input.GetAxisRaw("Horizontal");

        // Apply horizontal velocity based on input if not crouching
        if (!isCrouching)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement when crouching
        }

        // Check for jumping input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            Jump();
        }
    }

    private void Die()
    {
        // Trigger death animation or game over mechanics here
        anim.SetTrigger("Die");  // Assuming you have a "Die" animation trigger
        // Disable player movement or set an inactive state
        this.enabled = false;
        Debug.Log("Player has died!");
    }

    private void Jump()
    {
        // Apply vertical jump force only if grounded and not crouching
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

    private void HandleCrouch()
    {
        // Check for crouch input
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded) // Change key as needed
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StandUp();
        }
    }

    private void Crouch()
    {
        isCrouching = true;
        collider.size = new Vector2(collider.size.x, crouchHeight); // Adjust collider size
        anim.SetBool("isCrouching", true); // Set crouch animation
    }

    private void StandUp()
    {
        isCrouching = false;
        collider.size = new Vector2(collider.size.x, originalHeight); // Reset collider size
        anim.SetBool("isCrouching", false); // Reset crouch animation
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check raycast in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
