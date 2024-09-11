using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingAstro : MonoBehaviour
{
    public float moveSpeed = 200f; // Speed of object's movement
    public float rotationSpeed = 100f; // Speed of the astronaut's rotation

    private Vector2 direction; // Current direction of movement
    private RectTransform rectTransform; // Reference to the UI's position (RectTransform)
    private Vector2 minScreenBounds;          // Minimum bounds of the screen (bottom-left)
    private Vector2 maxScreenBounds;          // Maximum bounds of the screen (top-right)
    private Vector2 uiElementSize;            // Size of the UI element for collision calculations

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial movement direction (randomly choose left/right and up/down)
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Get the RectTransform of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Get the size of the UI element (half extents for bounds checking)
        uiElementSize = rectTransform.rect.size / 2f;

        // Calculate screen bounds (in Canvas space)
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        minScreenBounds = -canvasRect.sizeDelta / 2f;  // Bottom-left corner of the canvas
        maxScreenBounds = canvasRect.sizeDelta / 2f;   // Top-right corner of the canvas
    }

    // Update is called once per frame
    void Update()
    {
        MoveAstro();
    }

    void MoveAstro()
    {
        // Move the astronaut UI element
        rectTransform.anchoredPosition += direction * moveSpeed * Time.deltaTime;

        // Get the current anchored position of the astronaut
        Vector2 pos = rectTransform.anchoredPosition;

        // Check for collision with screen bounds and reverse direction if needed
        if (pos.x - uiElementSize.x < minScreenBounds.x || pos.x + uiElementSize.x > maxScreenBounds.x)
        {
            direction.x = -direction.x;  // Reverse horizontal direction
        }
        if (pos.y - uiElementSize.y < minScreenBounds.y || pos.y + uiElementSize.y > maxScreenBounds.y)
        {
            direction.y = -direction.y;  // Reverse vertical direction
        }

        // Rotate the astronaut UI element around its center
        rectTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
