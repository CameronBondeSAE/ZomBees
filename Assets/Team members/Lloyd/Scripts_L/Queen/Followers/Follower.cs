using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;
using Random = System.Random;

public class Follower : MonoBehaviour, IFollower
{
    public Transform rotationTransform;
    private Transform target;

    public float maxSpeed;
    public float speed;
    public float angle;
    public float circleSize;

    public bool reverseDirection;

    public bool moveInX;
    public bool moveInY;
    public bool moveInZ;

    private Rigidbody rb;

    public LayerMask followerLayer;

    private float angleOffset;

    private void Start()
    {
        Begin();
    }

    public void Begin()
    {
        //angleOffset = Random.Range(0f, 360f);
        
        rb = GetComponent<Rigidbody>();

        reverseDirection = (UnityEngine.Random.value > 0.5f);
        moveInX = (UnityEngine.Random.value > 0.5f);
        moveInY = (UnityEngine.Random.value > 0.5f);
        moveInZ = (UnityEngine.Random.value > 0.5f);

        Physics.IgnoreLayerCollision(followerLayer, followerLayer, true);
    }

    public void SetRotationPoint(Transform swarmTransform)
    {
        rotationTransform = swarmTransform;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        target = rotationTransform;
        if (rb != null)
        {
            CircleMovement();
            //MoveToTarget();
        }
    }

    private void CircleMovement()
    {
        if (reverseDirection)
        {
            angle = Mathf.Repeat(angle - Time.deltaTime * speed + angleOffset, 360.0f);
        }
        else
        {
            angle = Mathf.Repeat(angle + Time.deltaTime * speed + angleOffset, 360.0f);
        }

        float x = rotationTransform.position.x;
        float y = rotationTransform.position.y;
        float z = rotationTransform.position.z;

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
        float distance = direction.magnitude;

        float force = 0f;
        if (distance > circleSize)
        {
            force = (distance - circleSize) * speed * Time.deltaTime;
        }
        else if (distance < circleSize)
        {
            force = (circleSize - distance) * speed * Time.deltaTime;
        }

        rb.AddForce(direction.normalized * force + direction.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = rotationTransform.position +
                                 circleSize * (transform.position - rotationTransform.position).normalized;
        Vector3 direction = targetPosition - transform.position;
        rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}