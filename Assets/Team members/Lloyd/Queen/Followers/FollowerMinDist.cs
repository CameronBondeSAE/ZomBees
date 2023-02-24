using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMinDist : MonoBehaviour
{
    public float minDistance;
    private Rigidbody rb;
    public Vector3 targetPos;
    private Vector3 myPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVector3(Vector3 pos)
    {
        
    }

    private void FixedUpdate()
    {
        myPos = transform.position;
        Vector3 directionTowardsTarget = (targetPos - myPos).normalized;
        float distance = Vector3.Distance(targetPos, myPos);
        if (distance < minDistance)
        {
            rb.AddForce(-directionTowardsTarget * (minDistance - distance), ForceMode.Acceleration);
        }
        else if (distance > minDistance)
        {
            rb.AddForce(directionTowardsTarget * (distance - minDistance), ForceMode.Acceleration);
        }
    }
}