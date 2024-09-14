using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    private bool playerInCollider = false;
    private bool canDamage = false;
    private bool playerHit = false;

  
    private void Update()
    {
        StartCoroutine(LightningStrikeSequence());
    }

    // Coroutine for the lightning strike sequence
    IEnumerator LightningStrikeSequence()
    {

        yield return new WaitForSeconds(0.75f);

        // Start the damaging phase
        canDamage = true;

        // Wait for the rest of the animation (strike duration)
        yield return new WaitForSeconds(0.833f);

        // End the damaging phase and destroy the lightning after it finishes
        canDamage = false;
        Destroy(gameObject);
    }

    // When player enters the strike zone (box collider)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = true;

            // Damage the player immediately if they're in the damaging phase
            if (canDamage)
            {
                DamagePlayer();
            }
        }
    }

    // When player stays in the strike zone
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDamage && playerInCollider)
        {
            DamagePlayer();
        }
    }

    // When player leaves the strike zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = false;
        }
    }

    // Function to apply damage to the player
    void DamagePlayer()
    {
        if (!playerHit) // Ensures player is only hit once
        {
            if (Metres.scrap > 0)
            {
                Metres.scrap = Mathf.Max(0, Metres.scrap - 20);  // Subtract scrap but don't go below zero
            }
        }

        playerHit = true; // Set the player as hit to prevent further damage   
    }
}
