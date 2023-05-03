using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

public class HalfZombeeTurnTowards : MonoBehaviour
{
    public Transform targetTransform;

    public Rigidbody rb;

    public float turnSpeed;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (targetTransform != null)
        {
            Vector3 targetDir = (targetTransform.position - transform.position).normalized;
            Vector3 torqueDirection = Vector3.Cross(transform.forward, targetDir);
            rb.AddTorque(torqueDirection * turnSpeed);
        }

        Vector3 rotateUpDir = Vector3.Cross(transform.up, Vector3.up);
        rb.AddTorque(rotateUpDir * turnSpeed);
    }
}
