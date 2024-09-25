using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    // Function to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Death effect
    protected virtual void Die()
    {
        // Common death behavior, e.g., particle effects
        Debug.Log("Enemy died.");
        Destroy(gameObject);  // Destroy enemy
    }

    // To be overridden in derived classes for movement
    protected virtual void Move()
    {
        // No movement for base class
    }

    // You can have a common Update function for all enemies if needed
    protected virtual void Update()
    {
        Move();  // Move only if it's a moving enemy
    }
}
