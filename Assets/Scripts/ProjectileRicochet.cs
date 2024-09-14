using System.Collections;
using UnityEngine;

public class ProjectileRicochet : MonoBehaviour
{
    public float speed = 5f; // Speed of the projectile
    private GameObject player;

    void Start()
    {
        // Find the player object (assuming the player is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with another projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // 50% chance of moving towards the player
            if (Random.value > 0.5f)
            {
                // Start moving towards the player
                StartCoroutine(MoveTowardsPlayer());
            }
        }
    }

    IEnumerator MoveTowardsPlayer()
    {
        // While the projectile exists and the player exists
        while (player != null && gameObject != null)
        {
            // Calculate the direction to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Move the projectile towards the player
            transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }
    }
    //DISCLAIMER This Script was primarily written with AI as of 13/09/2024
}
