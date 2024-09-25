using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet the enemy shoots
    public Transform firePoint;  // Point from which the enemy shoots
    public float shootInterval = 2f;  // How often the enemy shoots
    public float bulletSpeed = 5f;  // Speed of the bullet
    public int health = 100;  // Health of the enemy
    private Transform player;  // Reference to the player

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Start shooting periodically
        InvokeRepeating("Shoot", 1f, shootInterval);
    }

    void Shoot()
    {
        if (player != null)
        {
            // Create the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Make the bullet move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }

    // Method to take damage when hit by player's bullet
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // Handle death
    void Die()
    {
        Destroy(gameObject);  // Destroy the enemy when health reaches 0
    }
}
