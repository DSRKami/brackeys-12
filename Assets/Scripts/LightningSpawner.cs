using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightningSpawner : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float spawnInterval = 2f;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnLightningRoutine());
    }

    IEnumerator SpawnLightningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified spawn interval before spawning next
            SpawnLightning();
        }
    }

    void SpawnLightning()
    {
        // Get the camera's horizontal boundaries (extremes)
        float cameraLeftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float cameraRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Choose a random X position within the camera's bounds
        float randomX = Random.Range(cameraLeftEdge, cameraRightEdge);

        // Spawn the lightning at the random X position and Y position of 0
        Vector2 spawnPosition = new Vector2(randomX, 0f);
        Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);
    }
}
