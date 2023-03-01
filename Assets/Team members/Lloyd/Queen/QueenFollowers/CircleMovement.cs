using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public QueenEvent queenEvent;
    
    public Vector3 centerPoint;
    public float radius;
    public int numVectors;
    public float moveSpeed;
    public float maxSpeed;
    public float minDistance;

    private float distance;

    private List<Vector3> targetPoints = new List<Vector3>();
    private int currentVector = 0;
    private Rigidbody rb;
    private bool isWaiting = false;

    public bool negativeRadius;

    public bool reverseDirectionX;
    public bool reverseDirectionY;
    public bool reverseDirectionZ;
    public bool moveInX;
    public bool moveInY;
    public bool moveInZ;

    private float x = 1;
    private float y = 1;
    private float z = 1;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        
        float angleStep = 360f / numVectors;
        
        negativeRadius = (UnityEngine.Random.value > 0.5f);

            moveInX = (UnityEngine.Random.value > 0.5f);
        reverseDirectionX = (UnityEngine.Random.value > 0.5f);
        
        moveInY = (UnityEngine.Random.value > 0.5f);
        reverseDirectionY = (UnityEngine.Random.value > 0.5f);
        
        moveInZ = (UnityEngine.Random.value > 0.5f);
        reverseDirectionZ = (UnityEngine.Random.value > 0.5f);

        for (int i = 0; i < numVectors; i++)
        {
            float angle = i * angleStep;
            
            float perlinValue = Mathf.PerlinNoise(Time.time, Time.deltaTime);
            radius += perlinValue;

            if (negativeRadius)
                radius *= -1;
            

            if (moveInX)
                x = centerPoint.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            
            if (moveInY)
                y = centerPoint.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            if (moveInZ)
                z = centerPoint.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            
            if (reverseDirectionX)
                x *= -1;
            
            if(reverseDirectionY){
                y *= -1;}
            
            if(reverseDirectionZ)
                z *= -1;
            
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

            targetPoints.Add(new Vector3(centerPoint.x + x, centerPoint.y + y, centerPoint.z + z));
        }
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, targetPoints[currentVector]);
        MoveAroundCircle();
    }

    private void MoveAroundCircle()
    {
        if (!isWaiting)
        {
            Vector3 targetPoint = targetPoints[currentVector];
            Vector3 direction = (targetPoint - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            StartCoroutine(WaitForDistance(targetPoint));
        }
    }

    private IEnumerator WaitForDistance(Vector3 targetPoint)
    {
        while (distance > minDistance)
        {
            yield return null;
            distance = Vector3.Distance(transform.position, targetPoint);
        }

        if (distance <= minDistance)
        {
            Vector3 removedTarget = targetPoints[0];
            targetPoints.RemoveAt(0);
            targetPoints.Add(removedTarget);
            currentVector = (currentVector + 1) % targetPoints.Count;
            isWaiting = false;
        }
    }
}