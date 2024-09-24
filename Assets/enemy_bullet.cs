using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;  // Damage dealt to the player

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the bullet hit the player
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);  // Destroy the bullet after hitting the player
        }

        // Destroy the bullet if it hits anything else
        Destroy(gameObject);
    }
}
