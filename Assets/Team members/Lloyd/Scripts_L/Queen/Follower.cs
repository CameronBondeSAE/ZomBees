using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class Follower : MonoBehaviour, IFollower
{
    public Transform rotationPoint;
    public float speed;
    public float angle;
    public float circleSize;

    public bool reverseDirection;

    public bool moveInX;
    public bool moveInY;
    public bool moveInZ;

    private Rigidbody rb;

    public LayerMask layer;

    public void Begin()
    {
        rb = GetComponent<Rigidbody>();

        reverseDirection = (UnityEngine.Random.value > 0.5f);
        moveInX = (UnityEngine.Random.value > 0.5f);
        moveInY = (UnityEngine.Random.value > 0.5f);
        moveInZ = (UnityEngine.Random.value > 0.5f);

        Physics.IgnoreLayerCollision(layer, layer, true);
    }

    public void SetRotationPoint(Transform transform)
    {
        rotationPoint = transform;
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);

            CircleMovement();
            MoveToTarget();
        }
    }

    private void CircleMovement()
    {
        if (reverseDirection)
        {
            angle = Mathf.Repeat(angle - Time.deltaTime * speed, 360.0f);
        }
        else
        {
            angle = Mathf.Repeat(angle + Time.deltaTime * speed, 360.0f);
        }

        float x = rotationPoint.position.x;
        float y = rotationPoint.position.y;
        float z = rotationPoint.position.z;

        if (moveInX)
        {
            x += Mathf.Cos(angle * Mathf.Deg2Rad) * circleSize;
        }

        if (moveInY)
        {
            y += Mathf.Sin(angle * Mathf.Deg2Rad) * circleSize;
        }

        if (moveInZ)
        {
            z += Mathf.Sin(angle * Mathf.Deg2Rad) * circleSize;
        }

        Vector3 targetPosition = new Vector3(x, y, z);
        Vector3 direction = targetPosition - transform.position;
        Vector3 forceDirection = direction.normalized;

        float distance = direction.magnitude;
        if (distance > circleSize)
        {
            rb.AddForce(forceDirection * (distance - circleSize) * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (distance < circleSize)
        {
            rb.AddForce(forceDirection * (circleSize - distance) * speed * Time.deltaTime, ForceMode.VelocityChange);
        }

        rb.AddForce(direction.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = rotationPoint.position +
                                 circleSize * (transform.position - rotationPoint.position).normalized;
        Vector3 direction = targetPosition - transform.position;
        rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}