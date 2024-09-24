using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;  // The position where the bullets are spawned
    public GameObject bulletPrefab;  // The bullet prefab to instantiate
    public float bulletSpeed = 20f;  // Speed of the bullet

    void Update()
    {
        // Check for mouse button press (left click)
        if (Input.GetButtonDown("Fire1"))  // Fire1 is typically the left mouse button
        {
            Shoot();
        }
    }

    // Shoot function to instantiate the bullet
    void Shoot()
    {
        // Instantiate the bullet at the firePoint position
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Get the Rigidbody2D of the bullet and apply velocity to make it move
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;  // Fire in the direction the player is facing
    }
}
 