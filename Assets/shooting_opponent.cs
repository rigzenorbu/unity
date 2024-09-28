using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotter_opponent : MonoBehaviour
{
    public GameObject bullet;          // Reference to the bullet prefab
    public Transform bulletpos;        // The position where the bullet will be instantiated
    private float timer;               // Timer to control the shooting interval
    public float shootRange = 10f;     // The range within which the enemy can shoot
    private GameObject player;         // Reference to the player
    
    public int health = 100;
    public int scoreValue = 50; // The amount of score to give when this enemy is killed

    private NewBehaviourScript playerScript; // Reference to the player script
    void Start()
    {
        timer = 0;                     // Initialize timer
        playerScript = GameObject.FindWithTag("Player").GetComponent<NewBehaviourScript>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
        
    }  

    void Update()
    {
        // Check if the player is assigned and within shooting range
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= shootRange)
        {
            // Increment timer by the time passed since the last frame
            timer += Time.deltaTime;

            // Check if it's time to shoot
            if (timer > 2) // Change this value to adjust the shooting frequency
            {
                timer = 0;                 // Reset timer
                Shoot();                   // Call shoot function
            }
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the bullet position with no rotation
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
    }
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
        // Increase the player's score when the enemy dies
        playerScript.IncreaseScore(scoreValue);

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
