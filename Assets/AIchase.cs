using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchase : MonoBehaviour
{
    public GameObject player;         // Reference to the player
    public float speed = 5f;          // Speed of the enemy movement
    private float distance;           // Distance between enemy and player
    public float chaseRange = 10f;    // Range within which enemy starts chasing
    public int health = 100;          // Health of the enemy
    public int scoreValue = 20;       // Score to be added when this enemy is killed

    private NewBehaviourScript playerScript; // Reference to the player script

    void Start()
    {
        // Find the player script (assuming the player has the tag "Player")
        playerScript = GameObject.FindWithTag("Player").GetComponent<NewBehaviourScript>();
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // Only chase if the player is within the chase range
        if (distance < chaseRange)
        {
            // Calculate direction towards the player and normalize it
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    // This function handles taking damage from the bullet
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
        // Increase the player's score when the enemy dies
        if (playerScript != null)
        {
            playerScript.IncreaseScore(scoreValue);
        }
        else
        {
            Debug.LogWarning("Player script not found!");
        }

        // Add death effect here if needed

        Destroy(gameObject);  // Destroy the enemy GameObject
    }
}
