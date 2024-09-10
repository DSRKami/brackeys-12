using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sync the position of object with target
        transform.position = target.position;

        // Reset the rotation to avoid following the rocket's rotation
        transform.rotation = Quaternion.identity;
    }
}
