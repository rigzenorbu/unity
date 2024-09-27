using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force; // Set a default force value
    public int bulletDamage = 10; // Amount of damage dealt by the bullet

    void Start()
    {
        // Initialize Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Find the player object with the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Ensure the player was found
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
        
        // Destroy the bullet after a certain time to avoid cluttering the scene
        Destroy(gameObject, 5f); // Adjust the lifetime as necessary
    }

    // Check for collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the player
        if (collision.CompareTag("Player"))
        {
            // Get the player's script (NewBehaviourScript)
            NewBehaviourScript playerHealth = collision.GetComponent<NewBehaviourScript>();

            // Check if the player has the NewBehaviourScript component
            if (playerHealth != null)
            {
                // Reduce player's health by bulletDamage
                playerHealth.TakeDamage(bulletDamage);
            }

            // Destroy the bullet on hit
            Destroy(gameObject);
        }
    }
}
