using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Public Fields for adjusting settings in the Unity Editor
    public float rotationSpeed = 200f;
    public float thrustForce = 5f;
    public float boostForce = 10f;
    public float gravityForce = 1f;
    public float finalGForce = 3f;

    public bool varyGravity;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isBoosting = false;
    private bool isUsingFuel = false;


    public Color rotationColour = Color.cyan;
    public float flashDuration = 0.1f;
    private Color originalColour;
    private SpriteRenderer sprite;
    public GameObject locked;

    // Start is called before the first frame update
    void Start()
    {
        locked.SetActive(false);

        sprite = GetComponent<SpriteRenderer>();
        originalColour = sprite.color;

        // Get the Rigidbody2D component attached to the rocket
        rb = GetComponent<Rigidbody2D>();

        // Get the Animator component attached to the rocket
        animator = GetComponent<Animator>();

        if (varyGravity)
        {
            // Start the gravity variation over 60 seconds
            StartCoroutine(Approach.LerpValues(gravityForce, finalGForce, 60f, UpdateGravityForce));
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsBoosting", isBoosting);
        animator.SetBool("IsUsingFuel", isUsingFuel);

        ApplyRotation();
        ApplyGravity();
        RocketMove();

        if (Input.GetKeyDown(KeyCode.S))
        {
            ResetVelocity(); // Velocity Reset mechanic
        }
    }

    // Rotates the rocket based on player input
    void ApplyRotation()
    {
        // Get Input Direction
        float rotationInput = Input.GetAxis("Horizontal");

        if (rotationInput != 0)
        {
            // Apply rotation based on the input and rotation speed
            float rotation = rotationInput * rotationSpeed * Time.deltaTime;
            rb.MoveRotation(rb.rotation - rotation); // Subtracting to make it rotate as expected
        }
        else
        {
            // Stop the rocket's rotation when there's no input
            rb.angularVelocity = 0f;
        }
    }

    // Applies thrust in the direction the rocket is facing
    void RocketMove()
    {
        if (Input.GetKey(KeyCode.Space) && Metres.fuel > 1)
        {
            Metres.fuel -= 0.1f;
            ApplyThrust(ref boostForce, ref isUsingFuel);
            return;
        }
        else
        {
            // Put animation in idle state
            isUsingFuel = false;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            ApplyThrust(ref thrustForce, ref isBoosting);
        }
        else
        {
            // Put animation in idle state
            isBoosting = false;
        }
    }

    // Simulates gravity by applying a constant downward force
    void ApplyGravity()
    {
        // Apply a constant force downwards
        rb.AddForce(Vector2.down * gravityForce);
    }

    void ApplyThrust(ref float force, ref bool animation)
    {
        // Calculate the direction the rocket is facing
        Vector2 thrustDirection = transform.up;

        // Apply force in the direction of thrust
        rb.AddForce(thrustDirection * force);

        // Update the Animation to show Thrust
        animation = true;
    }

    void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.rotation = 0f;

        StartCoroutine(ResetVelocityCoroutine());
        StartCoroutine(LockCoroutine());
    }

    IEnumerator ResetVelocityCoroutine()
    {
        // Change the player's color to the flash color
        sprite.color = rotationColour;
        yield return new WaitForSeconds(flashDuration);
        sprite.color = originalColour;
    }

    IEnumerator LockCoroutine()
    {
        locked.SetActive(true);
        yield return new WaitForSeconds(0.3333f);
        locked.SetActive(false);
    }

    // Callback function to update gravityForce as LerpValue progresses
    void UpdateGravityForce(float newGravityValue)
    {
        gravityForce = newGravityValue;
    }
}
