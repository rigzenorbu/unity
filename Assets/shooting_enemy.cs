using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{
    public Transform player;    // Reference to the player
    public float speed = 3f;    // Enemy movement speed
    public float attackRange = 5f;   // Distance at which the enemy will stop and start shooting

    public GameObject bulletPrefab;  // Bullet prefab for shooting
    public Transform firePoint;      // The point from which the enemy fires bullets
    public float bulletSpeed = 10f;
    public float fireRate = 2f;      // How often the enemy fires
    private float nextFireTime = 0f;

    private bool facingRight = true; // Track facing direction

    void Start()
    {
        // Attempt to find the player by tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Using null conditional operator

        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the Player GameObject is tagged as 'Player'.");
        }
    }

    void Update()
    {
        // Move towards the player if not within attack range
        if (player != null) // Check if player is assigned
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                // Stop and shoot at the player if within range
                if (Time.time > nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate;  // Set the next fire time
                }
            }

            // Handle facing direction (so the enemy faces the player)
            if ((player.position.x < transform.position.x && facingRight) || 
                (player.position.x > transform.position.x && !facingRight))
            {
                Flip();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void Shoot()
    {
        // Instantiate a bullet at the firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Add force to the bullet to shoot it towards the player
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - firePoint.position).normalized;  // Calculate direction towards player
        rb.velocity = direction * bulletSpeed;  // Shoot the bullet towards the player
    }

    private void Flip()
    {
        // Flip the enemy to face the player
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);  // Rotate 180 degrees on the Y-axis to flip
    }
}
