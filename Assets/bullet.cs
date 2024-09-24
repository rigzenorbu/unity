using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 20;  // Damage dealt to the enemy

    // Method that gets called when the bullet hits a trigger
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the object hit has an EnemyController component
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  // Apply damage to the enemy
            Destroy(gameObject);  // Destroy the bullet after hitting the enemy
        }

        // Destroy the bullet if it hits anything else (optional)
        Destroy(gameObject);
    }
}
