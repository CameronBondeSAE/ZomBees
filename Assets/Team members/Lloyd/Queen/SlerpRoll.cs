using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpRoll : MonoBehaviour
{
    public float torqueAmount; // The amount of torque to apply
    public float slerpSpeed; // The speed of slerping towards zero
    public float damping; // The damping value to use

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate the torque values for each axis based on the current rotation of the object
        float xTorque = transform.rotation.x * torqueAmount;
        float zTorque = transform.rotation.z * torqueAmount;

        // Calculate the target torque based on the current rotation of the object and the damping value
        Vector3 targetTorque =
            new Vector3(-xTorque, 0.0f, -zTorque) * Mathf.Clamp01(1.0f - damping * Time.fixedDeltaTime);

        // Slerp towards zero on the z and x axis
        rb.AddTorque(Vector3.Slerp(rb.angularVelocity, targetTorque, slerpSpeed * Time.fixedDeltaTime));
    }
}