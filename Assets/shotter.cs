using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotter : MonoBehaviour
{
    public GameObject bullet;          // Reference to the bullet prefab
    public Transform bulletpos;        // The position where the bullet will be instantiated
    private float timer;               // Timer to control the shooting interval
    public float shootRange = 10f;     // The range within which the enemy can shoot
    private GameObject player;         // Reference to the player

    void Start()
    {
        timer = 0;                     // Initialize timer
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
}
