using System.Collections;
using System.Collections.Generic;
using Marcus;
using UnityEngine;

public class BasicFlyDudeMovement : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = GetComponent<FlyDudeStats>().speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * moveSpeed);
    }
}
