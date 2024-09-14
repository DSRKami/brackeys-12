using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public float fuelIncreaseAmount = 10f;
    public float energyIncreaseAmount = 5f;
    public float scrapIncreaseAmount = 20f;

    private Animator animator;
    private bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided is the player's spaceship
        if (other.CompareTag("Player") && !isCollected)
        {
            // Set the object as collected to prevent multiple triggers
            isCollected = true;

            // Play the destruction animation
            animator.SetTrigger("Collect");

            // Call the method to increase player's resources
            IncreasePlayerResources();

            // Destroy the object after the animation finishes
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    void IncreasePlayerResources()
    {
        // Increase player's fuel, energy, and scrap by the specified amounts
        Metres.fuel = Mathf.Clamp(Metres.fuel + fuelIncreaseAmount, 0, Metres.maxFuel);
        Metres.energy = Mathf.Clamp(Metres.energy + energyIncreaseAmount, 0, Metres.maxEnergy);
        Metres.scrap = Mathf.Clamp(Metres.scrap + scrapIncreaseAmount, 0, Metres.maxScrap);
    }

    IEnumerator DestroyAfterAnimation()
    {
        // Wait for the length of the animation (assumes the animation is 1 second long, adjust as needed)
        yield return new WaitForSeconds(0.58f);

        // Destroy the object after the animation plays
        Destroy(gameObject);
    }
}
