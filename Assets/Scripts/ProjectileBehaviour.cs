using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject self; 
    public GameObject player; 
    public Rigidbody2D rb; 
    public float projectileSpeed; 
    public float TimeToSelfDestruct; 
    public int ProjectileType;
    public Animator AsteroidAnim; 

    void Start()
    {
        AsteroidAnim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find player in the scene

        // Get the direction towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Apply velocity in the direction of the player
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;

        // Rotate the asteroid to face the player
        RotateTowardsPlayer(direction);
    }

    // Rotate the projectile to face the player
    void RotateTowardsPlayer(Vector3 direction)
    {
        // Calculate the angle between the projectile and the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the projectile to face the player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the projectile hit the player
        {
            AsteroidAnim.Play("AsteroidExplosion"); 
            
            Debug.Log("Player Hit");

            // Optionally, you can trigger destruction or an effect here
            Invoke("SelfDestruct", 0.6f); // Delay the self-destruction by 1 second after collision
        }
    }

    void Update()
    {
      
        Invoke("SelfDestruct", TimeToSelfDestruct);
    }

 
    void SelfDestruct()
    {
        self.SetActive(false); // Disable the projectile
    }
}
//DISCLAIMER :AI assistance was used for section required to keep the asteroid's rotation to face the player