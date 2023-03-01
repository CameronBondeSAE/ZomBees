using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Tanks;

public class MoveForwards : AntAIState
{
    private Rigidbody rb;

    private GameObject movePoint;

    public float minDist;

    private float moveSpeed;
    private float maxSpeed;

    public Stats stats;

    private void OnEnable()
    {
        rb = GetComponentInParent<Rigidbody>();

        stats = GetComponentInParent<Stats>();
        moveSpeed = stats.moveSpeed;
        maxSpeed = stats.maxMoveSpeed;
    }

    public void SetTarget(GameObject target)
    {
        movePoint = target;
    }

    private IEnumerator LerpTowards()
    {
        while (true)
        {
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(transform.position, movePoint.transform.position);
            while (!Mathf.Approximately(journeyLength, 0f) && journeyLength > minDist)
            {
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fracJourney = distCovered / journeyLength;
                Vector3 targetPosition = Vector3.Lerp(transform.position, movePoint.transform.position, fracJourney);

                Vector3 direction = (targetPosition - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, targetPosition);

                float forceMagnitude = Mathf.Clamp(distance / Time.fixedDeltaTime, 0f, maxSpeed);
                Vector3 force = direction * forceMagnitude;

                rb.AddForce(force, ForceMode.VelocityChange);

                yield return null;
                journeyLength = Vector3.Distance(transform.position, movePoint.transform.position);
            }
        }
    }

}
