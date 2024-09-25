using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Speed of the bullet
    public float speed = 20f;
    public int damage = 20;

    // Rigidbody2D of the bullet
    public Rigidbody2D rb;

    void Start()
    {
        // Apply velocity to the Rigidbody2D
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the hit object has an enemystatus component
        enemystatus enemyStatus = hitInfo.GetComponent<enemystatus>();
        if (enemyStatus != null)
        {
            enemyStatus.TakeDamage(damage);
            Destroy(gameObject); // Destroy the bullet after hitting
            return; // Exit the method to prevent further checks
        }

        // Check if the hit object has an AIchase component
        AIchase enemyAI = hitInfo.GetComponent<AIchase>();
        if (enemyAI != null)
        {
            enemyAI.TakeDamage(damage);
            Destroy(gameObject); // Destroy the bullet after hitting
        }
    }
}
