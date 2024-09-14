using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private Animator animator;
    public float deathDuration;
    public LevelLoader loader;
    private bool isDead = false;

    private SpriteRenderer sprite;
    public Color flashColour = Color.red;
    public float flashDuration = 0.1f;
    private Color originalColour;
    private float currentScrap;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        currentScrap = Metres.scrap;
        animator = GetComponent<Animator>();
        Metres.scrap += 1;
        originalColour = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        TakeDamage();
    }

    void CheckDeath()
    {
        if (Metres.scrap < 1 && !isDead)
        {
            // Start death sequence
            StartCoroutine(HandleDeath());
            Debug.Log("Death Applied");
        }
    }

    IEnumerator HandleDeath()
    {
        isDead = true;

        // Play the player's death animation
        animator.SetTrigger("Die");

        // Wait for death animation to play
        yield return new WaitForSeconds(deathDuration);

        // Reset all metres
        Metres.fuel = 0;
        Metres.scrap = 0;
        Metres.energy = 0;

        loader.LoadLevel(0);
    }

    void TakeDamage()
    {
        if (Metres.scrap < currentScrap)
        {
            StartCoroutine(FlashRoutine());
        }

        currentScrap = Metres.scrap;
    }

    IEnumerator FlashRoutine()
    {
        // Change the player's color to the flash color
        sprite.color = flashColour;

        // Wait for the specified flash duration
        yield return new WaitForSeconds(flashDuration);

        // Change the player's color back to the original
        sprite.color = originalColour;
    }
}
