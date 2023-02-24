using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using UnityEngine;

public class QueenPathfinding : AntAIState
{
    // Queen has a List of GameObjects flyPoints which is as large as numFlyPoints
    // Queen get s a reference to the next flyPoint and stores it as currentFlyPoint, and the old previousFlyPoint

    // Queen uses LerpTowards to move itself to the currPoint. when it is within the float minDist, it changes points

    // multiple Lists for variable paths?

    public List<GameObject> flyPoints;
    public GameObject prevFlyPoint;
    public GameObject currFlyPoint;
    
    public float flySpeed;
    public float curSpeed;
    public float maxSpeed;
    public float minDist;

    public float turnSpeed;
    public float maxTorque;
    
    public bool isMoving;

    private Rigidbody rb;

    public override void Enter()
    {
        rb = GetComponent<Rigidbody>();

            if (flyPoints.Count > 0)
        {
            InitialiseList();
            StartCoroutine(LerpTowards());
        }
        else
        {
            Debug.LogError("FlyPoints list is empty!");
            isMoving = false;
        }

        isMoving = true;
    }

    void InitialiseList()
    {
        currFlyPoint = flyPoints[0];
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        
        curSpeed = rb.velocity.magnitude;

        //QueenTurn();
    }

    private void QueenTurn()
    {
        Vector3 targetDir = currFlyPoint.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, transform.up);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * turnSpeed);
        rb.MoveRotation(rotation);

        Vector3 rotationAxis = Vector3.Cross(transform.forward, targetDir);
        float rotationDirection = Mathf.Sign(Vector3.Dot(rotationAxis, transform.up));
        float rotationMagnitude = rotationDirection * Mathf.Min(rotationAxis.magnitude, maxTorque);
        Vector3 rotationForce = rotationMagnitude * rotationAxis.normalized;

        // Add the torque to the Rigidbody
        rb.AddTorque(rotationForce, ForceMode.Force);
    }

    private IEnumerator LerpTowards()
    {
        while (isMoving)
        {
            prevFlyPoint = currFlyPoint;
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(transform.position, currFlyPoint.transform.position);
            while (!Mathf.Approximately(journeyLength, 0f) && journeyLength > minDist)
            {
                float distCovered = (Time.time - startTime) * flySpeed;
                float fracJourney = distCovered / journeyLength;
                Vector3 targetPosition = Vector3.Lerp(transform.position, currFlyPoint.transform.position, fracJourney);

                // Calculate the direction and distance to the target position
                Vector3 direction = (targetPosition - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, targetPosition);

                // Calculate the force to apply
                float forceMagnitude = Mathf.Clamp(distance / Time.fixedDeltaTime, 0f, maxSpeed);
                Vector3 force = direction * forceMagnitude;

                // Apply the force to the rigidbody
                rb.AddForce(force, ForceMode.VelocityChange);

                yield return null;
                journeyLength = Vector3.Distance(transform.position, currFlyPoint.transform.position);
            }

            if (flyPoints.Count > 1)
            {
                int currIndex = flyPoints.IndexOf(currFlyPoint);
                currFlyPoint = flyPoints[(currIndex + 1) % flyPoints.Count];
            }
            else
            {
                currFlyPoint = flyPoints[0];
            }
        }
    }

    private void OnDisable()
    {
        isMoving = false;
    }
}