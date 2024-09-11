using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;

    public float timer;
    public float interval; 
    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // For every frame, timer is incremented by + Time.deltaTime. Time.deltaTime represents the time between the last frame and current frame. 
        
        if(timer > interval) 
        {
            timer = 0;
            shoot(); 
        }
    }

    void shoot() 
    {

        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    
    }
}
