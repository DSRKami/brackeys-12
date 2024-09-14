using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;  // Base speed of the scrolling
    public float finalScrollSpeed = 0.1f; // Final speed of scrolling
    public bool varySpeed; // Flag to whether or not speed will be varied

    public Vector2 scrollDirection = new Vector2(1, -1);  // Scroll direction which is down-right by default

    private Material bgMaterial; // Material of the background's sprite renderer
    private Vector2 offset; // Offset value for scrolling

    void Start()
    {
        bgMaterial = GetComponent<SpriteRenderer>().material;

        if (varySpeed)
        {
            // Start the variation in background speed with approach to the planet
            StartCoroutine(Approach.LerpValues(scrollSpeed, finalScrollSpeed, 60f, UpdateScrollSpeed));
        }
    }

    void Update()
    {
        ScrollBackground();
    }

    void ScrollBackground()
    {
        // Normalize the scroll direction for consistent movement
        Vector2 normalizedScrollDir = scrollDirection.normalized;

        // Calculate the final scroll offset based on the direction and constant speed
        offset = normalizedScrollDir * scrollSpeed * Time.deltaTime;

        // Update the material's texture offset to create the scrolling effect
        bgMaterial.mainTextureOffset += offset;
    }

    void UpdateScrollSpeed(float newScrollSpeed)
    {
        scrollSpeed = newScrollSpeed;
    }
}

