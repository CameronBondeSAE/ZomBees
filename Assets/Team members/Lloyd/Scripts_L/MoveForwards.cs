using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class MoveForwards : MonoBehaviour
{

    private Rigidbody rb;

    public float walkSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * walkSpeed);
    }

}
