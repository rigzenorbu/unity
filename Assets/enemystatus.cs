using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystatus : MonoBehaviour
{
    public int health = 100;

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
}
