using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject self;
    public GameObject player;
    public Rigidbody2D rb;
    public float projectileSpeed;
    public float TimeToSelfDestruct;
    public int ProjectileType; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the direction towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Apply velocity in the direction of the player
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            Invoke("SelfDestruct", 1f); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("SelfDestruct", TimeToSelfDestruct);
    }

    void SelfDestruct()
    {
        self.SetActive(false);
    }
}
