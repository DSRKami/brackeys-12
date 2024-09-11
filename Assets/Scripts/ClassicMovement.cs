using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Movement speed of the player
    private Rigidbody2D rb;        
    private Vector2 movement;      

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");  // Left/Right movement (A/D or Left/Right Arrow)
        movement.y = Input.GetAxisRaw("Vertical");    // Up/Down movement (W/S or Up/Down Arrow)

        // Move the player based on the input and speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
