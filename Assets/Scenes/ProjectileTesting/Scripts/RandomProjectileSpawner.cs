using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to spawn
    public GameObject player; // Reference to the player
    public float spawnRadius;
    public float spawnInterval; 

    private void Start()
    {
        // Start the repeating spawning process
        InvokeRepeating("SpawnProjectile", 0f, spawnInterval);
    }

    void SpawnProjectile()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        // Calculate the position on the circumference of the circle
        Vector2 spawnPos = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * spawnRadius;

        // Calculate the world position by adding the spawn point to the spawner's position
        Vector2 spawnPosition = (Vector2)transform.position + spawnPos;

        // Instantiate the projectile at the calculated position
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // The projectile's behavior (such as speed and self-destruction) will be handled by its own script
    }

    // Visualize the spawn area using Gizmos
    private void OnDrawGizmos()
    {
        // Set the color of the gizmo circle
        Gizmos.color = Color.green;

        // Draw a wireframe sphere (2D circle) at the spawner's position
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
