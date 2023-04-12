using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;
using UnityEngine.VFX;
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

    private LookAtTarget lookAt;

    private CircleMovement circleMovement;

    private FollowerMinDist minDist;

    private BeeWingsManager beeWings;

    private void OnEnable()
    {
        beeWings = GetComponentInChildren<BeeWingsManager>();
        beeWings.SpawnWings();
        Begin();
    }

    public void Begin()
    {
        //angleOffset = Random.Range(0f, 360f);
        circleMovement = GetComponent<CircleMovement>();

        minDist = GetComponent<FollowerMinDist>();

        lookAt = GetComponent<LookAtTarget>();

        rb = GetComponent<Rigidbody>();

        reverseDirection = (UnityEngine.Random.value > 0.5f);

        moveInX = UnityEngine.Random.value > 0.5f;
        moveInY = UnityEngine.Random.value > 0.5f;
        moveInZ = UnityEngine.Random.value > 0.5f;

        int count = (moveInX ? 1 : 0) + (moveInY ? 1 : 0) + (moveInZ ? 1 : 0);
        if (count < 2) {
            if (!moveInX) {
                moveInX = true;
            } else if (!moveInY) {
                moveInY = true;
            } else {
                moveInZ = true;
            }
        }
    }

    public void SetRotationPoint(Transform swarmTransform)
    {
        rotationTransform = swarmTransform;
        circleMovement.SetCenterPoint(swarmTransform);
        lookAt.SetTarget(swarmTransform);
    }

    public void SetCircleSize(float newCircleSize)
    {
        circleSize = newCircleSize;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        target = rotationTransform;
        if (rb != null)
        {
            MoveToTarget();
            lookAt.SetTarget(target);
            circleMovement.SetCenterPoint(target);
            minDist.SetTarget(target);
        }
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = rotationTransform.position +
                                 circleSize * (transform.position - rotationTransform.position).normalized;
        Vector3 direction = targetPosition - transform.position;
        rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}