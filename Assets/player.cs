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
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    private int facingDirection = 1;
    private bool facingRight = true;

   

    void Start()
    {
        // Optional initialization if needed
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Get horizontal input for movement
        xInput = Input.GetAxisRaw("Horizontal");  
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
       
       isMoving = rb.velocity.x!=0;
       anim.SetBool("isMoving",isMoving);

       if(Input.GetKeyDown(KeyCode.R))
           Flip();

        FlipController();
    }

    private void Flip()
    {
        facingDirection = facingDirection* -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    private void FlipController()
    {
        if(rb.velocity.x>0 && !facingRight)
        {
            Flip();
        }
        else if(rb.velocity.x<0 && facingRight)
        {
            Flip();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y-groundCheckDistance));
    }

    
    //Debug.Log(isGrounded);
}
