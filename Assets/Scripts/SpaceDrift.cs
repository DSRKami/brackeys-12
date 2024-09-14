using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDrift : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject prefab;
        public float rarity; // Percentage chance of spawning (e.g. 10f means 10%)
    }

    public SpawnableObject[] objectsToSpawn; // Array of spawnable objects
    public float spawnRangeOutsideCamera = 10f; // Distance outside the camera where objects spawn
    public float spawnInterval = 3f; // Time interval between spawns
    public float despawnCheckTime = 5f; // Time interval to check for despawns
    public float lifespan = 30f; // Unified lifespan for all objects before they are destroyed

    public float minSpawnSpeed = 2f;
    public float maxSpawnSpeed = 5f;
    public float minTorque = -10f; // Minimum rotational velocity (torque)
    public float maxTorque = 10f;  // Maximum rotational velocity (torque)

    public float directionOffsetRange = 0.5f; // Random offset range for object direction

    private Transform player; // Reference to the player position
    private Camera mainCamera;
    private float screenHalfHeight;
    private float screenHalfWidth;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        screenHalfHeight = mainCamera.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCamera.aspect;

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomObject()
    {
        // Randomly select object based on rarity
        GameObject selectedPrefab = GetRandomPrefabByRarity();
        if (selectedPrefab == null) return;

        // Generate random spawn position outside the camera's view
        Vector2 spawnPosition = GetRandomSpawnPositionOutsideCamera();

        // Instantiate the object
        GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // Assign velocity towards the player's area (camera area)
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calculate direction towards the player
            Vector2 directionToPlayer = (player.position - spawnedObject.transform.position).normalized;

            // Add random offset to the direction
            Vector2 randomOffset = new Vector2(Random.Range(-directionOffsetRange, directionOffsetRange), Random.Range(-directionOffsetRange, directionOffsetRange));
            directionToPlayer += randomOffset;
            directionToPlayer.Normalize(); // Normalize after applying offset

            // Set the velocity with the random speed
            float randomSpeed = Random.Range(minSpawnSpeed, maxSpawnSpeed);
            rb.velocity = directionToPlayer * randomSpeed;

            // Apply random rotational velocity (torque)
            float randomTorque = Random.Range(minTorque, maxTorque);
            rb.AddTorque(randomTorque, ForceMode2D.Impulse);
        }

        // Destroy the object after the unified lifespan ends
        Destroy(spawnedObject, lifespan);

        // Add to the list of spawned objects for later despawn checks
        spawnedObjects.Add(spawnedObject);
    }

    GameObject GetRandomPrefabByRarity()
    {
        float totalRarity = 0f;

        // Calculate total rarity
        foreach (SpawnableObject obj in objectsToSpawn)
        {
            totalRarity += obj.rarity;
        }

        float randomValue = Random.Range(0f, totalRarity);
        float cumulativeRarity = 0f;

        // Select object based on rarity
        foreach (SpawnableObject obj in objectsToSpawn)
        {
            cumulativeRarity += obj.rarity;
            if (randomValue <= cumulativeRarity)
            {
                return obj.prefab;
            }
        }

        return null; // Shouldn't happen unless no objects have rarity > 0
    }

    Vector2 GetRandomSpawnPositionOutsideCamera()
    {
        // Randomly select a position outside the camera view
        float spawnX = Random.Range(-screenHalfWidth - spawnRangeOutsideCamera, screenHalfWidth + spawnRangeOutsideCamera);
        float spawnY = Random.Range(-screenHalfHeight - spawnRangeOutsideCamera, screenHalfHeight + spawnRangeOutsideCamera);

        // Ensure the object spawns outside the camera's view
        if (Mathf.Abs(spawnX) < screenHalfWidth && Mathf.Abs(spawnY) < screenHalfHeight)
        {
            if (Random.value > 0.5f)
                spawnX = (spawnX > 0 ? screenHalfWidth : -screenHalfWidth) + spawnRangeOutsideCamera;
            else
                spawnY = (spawnY > 0 ? screenHalfHeight : -screenHalfHeight) + spawnRangeOutsideCamera;
        }

        return new Vector2(player.position.x + spawnX, player.position.y + spawnY);
    }
}
