using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public GameObject player;
    public float spawnRadius;
    public float spawnInterval; 

    private void Start()
    {
        // Start the repeating spawning process
        InvokeRepeating("SpawnProjectile", 0f, spawnInterval);
    }

    void SpawnProjectile()
    {
        // Generate a random position within a circle with the specified radius
        Vector2 randomPos = Random.insideUnitCircle * spawnRadius;

        // Instantiate the projectile at the random position
        GameObject projectile = Instantiate(projectilePrefab, (Vector2)transform.position + randomPos, Quaternion.identity);

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
