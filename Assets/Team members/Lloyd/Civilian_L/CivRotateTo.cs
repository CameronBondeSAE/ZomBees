using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class CivRotateTo : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 180f;
    public float stopAngle = 1f;
    public float decelerationAngle = 15f;
    public bool inRange = false;

    private CivSensor civBrain;
    
    private Rigidbody rb;

    private void Start()
    {
        civBrain = GetComponent<CivSensor>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        target = civBrain.RotateToTarget;
        if (target == null) return;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, transform.right);
        float angle = Quaternion.Angle(transform.rotation, targetRotation);
        float direction = 1f;

        if (angle > stopAngle)
        {
            if (Quaternion.Angle(transform.rotation * Quaternion.Euler(0f, 1f, 0f), targetRotation) < angle)
            {
                direction = -1f;
            }

            if (angle <= decelerationAngle)
            {
                float decelerationFactor = Mathf.Clamp01((angle - stopAngle) / (decelerationAngle - stopAngle));
                rb.AddTorque(transform.up * rotateSpeed * decelerationFactor * direction, ForceMode.Acceleration);
            }
            else
            {
                rb.AddTorque(transform.up * rotateSpeed * direction, ForceMode.Acceleration);
            }
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
            inRange = true;
        }
    }
}
