using System.Collections;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // The point from where the bullet will be fired
    public float moveSpeed = 2f; // Speed of the enemy movement
    public float shootInterval = 2f; // Time between shots
    public float moveDistance = 3f; // Distance the enemy will move back and forth
    public float chaseRange = 10f; // Range within which the enemy can shoot
    public int health = 100; // Health of the enemy
    public Transform player; // Reference to the player

    private Vector3 startPosition; // The starting position of the enemy

    void Start()
    {
        startPosition = transform.position; // Store the starting position
        StartCoroutine(ShootRoutine()); // Start the shooting routine
    }

    void Update()
    {
        MoveBackAndForth(); // Handle enemy movement
    }

    private void MoveBackAndForth()
    {
        // Move the enemy back and forth within the specified distance
        float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Check if the player is within the chase range
        if (Vector2.Distance(transform.position, player.position) < chaseRange)
        {
            // Look at the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.right = direction; // Rotate the enemy to face the player
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            // Check if the player is within shooting range
            if (Vector2.Distance(transform.position, player.position) < chaseRange)
            {
                Shoot(); // Shoot at the player
            }
            yield return new WaitForSeconds(shootInterval); // Wait for the specified interval
        }
    }

    private void Shoot()
    {
        // Instantiate the bullet prefab at the fire point
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
