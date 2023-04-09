using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUpright : MonoBehaviour
{
    public Rigidbody rb;
    public float torqueStrength = 1.0f;
    public float uprightThreshold = 5.0f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Check if the rigidbody is tilted more than the upright threshold
        if (Vector3.Angle(rb.transform.up, Vector3.up) > uprightThreshold)
        {
            Vector3 rigidbodyUp = transform.InverseTransformDirection(rb.transform.up);

            float torqueZ = rigidbodyUp.z;

            Vector3 torque = new Vector3(0, 0, torqueZ) * torqueStrength;
            rb.AddTorque(torque, ForceMode.Force);
        }
    }
}