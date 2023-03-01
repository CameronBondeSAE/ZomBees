using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using HarmonyLib;
using Lloyd;
using UnityEngine;

public class QueenLerpTowards : MonoBehaviour
{
    // Queen has a List of GameObjects flyPoints which is as large as numFlyPoints
    // Queen get s a reference to the next flyPoint and stores it as currentFlyPoint, and the old previousFlyPoint

    // Queen uses LerpTowards to move itself to the currPoint. when it is within the float minDist, it changes points

    // multiple Lists for variable paths?

    public List<GameObject> flyPoints;
    
    public GameObject prevFlyPoint;
    
    public GameObject currFlyPoint;

    public bool patrol;

    public float flySpeed;
    
    public float curSpeed;
    public float maxSpeed;
    public float minDist;

    public float turnSpeed;
    public float maxTorque;

    public bool isMoving;

    private Rigidbody rb;

    private QueenScenarioManager queenScene;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

        queenScene = GetComponent<QueenScenarioManager>();

        if (patrol)
        {
            Patrol();
        }
    }

    public void Patrol()
    {
        currFlyPoint = flyPoints[0];
        isMoving = true;
        StartCoroutine(LerpTowards());
    }

    public void SetFlyPoint(GameObject lerpTarget)
    {
        currFlyPoint = lerpTarget;
        isMoving = true;
        StartCoroutine(LerpTowards());
    }

    private void FixedUpdate()
    {
        curSpeed = rb.velocity.magnitude;

        isMoving = queenScene.isMoving;

        if (isMoving)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        else
            rb.velocity = Vector3.zero;

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
        }

        if (flyPoints.Count == 0)
        {
            isMoving = false;
        }
    }
}