using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;  // Base speed of the scrolling
    public Vector2 scrollDirection = new Vector2(1, -1);  // Scroll direction, down-right by default

    private Material bgMaterial;          // Material of the background's sprite renderer
    private Vector2 offset;               // Offset value for scrolling

    void Start()
    {
        // Get the material of the background's sprite renderer
        bgMaterial = GetComponent<SpriteRenderer>().material;
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
}

