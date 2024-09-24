using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
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
        
        enemystatus enemy =  hitInfo.GetComponent<enemystatus>();
        if(enemy!=null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
} 
