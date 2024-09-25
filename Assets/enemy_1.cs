using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_1 : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 2f;  // Speed at which the enemy moves
    private Transform player;     // Reference to the player

    private bool isFacingRight = true; // To manage the facing direction

    void Start()
    {
        // Find the player object in the scene (make sure the player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    // Function to move the enemy toward the player
    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        
        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        
        // Flip the enemy's direction based on its movement
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    // This function is called to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // This function handles the enemy's death
    void Die()
    {
        // Create a simple death effect (e.g., a basic particle effect)
        GameObject deathEffect = new GameObject("DeathEffect"); // Create an empty GameObject
        ParticleSystem ps = deathEffect.AddComponent<ParticleSystem>(); // Add a Particle System component

        // Configure the particle system here (e.g., shape, size, duration)
        var main = ps.main;
        main.startLifetime = 0.5f;  // Lifetime of particles
        main.startSize = 0.5f;      // Size of particles
        main.startSpeed = 5f;       // Speed of particles

        // Additional particle system settings can be configured here

        ps.Play(); // Start the particle system

        // Destroy the GameObject after the effect duration
        Destroy(deathEffect, 1f); // Adjust the time as needed
        
        // Destroy the enemy GameObject
        Destroy(gameObject);
    }

    // Function to flip the enemy's direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
