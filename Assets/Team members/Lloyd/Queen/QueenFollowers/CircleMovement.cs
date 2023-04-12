using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform targetTransform;
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

    private float x;
    private float y;
    private float z;

    public float torqueSpeed;

    public bool following = false;

    public void SetCenterPoint(Transform target)
    {
        targetTransform = target;
        following = true;
    }

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
            float theta = i * Mathf.PI / numVectors;
            float phi = Random.Range(0f, 2f * Mathf.PI);

            float perlinValue = Mathf.PerlinNoise(Time.time, Time.deltaTime);
            radius += perlinValue;

            if (negativeRadius)
                radius *= -1;

           x = centerPoint.x + radius * Mathf.Sin(theta) * Mathf.Cos(phi);
           y = centerPoint.y + radius * Mathf.Sin(theta) * Mathf.Sin(phi); z = centerPoint.z + radius * Mathf.Cos(theta);

            targetPoints.Add(new Vector3(x, y, z));
        }
    }

    private void FixedUpdate()
    {
        if (following)
        {
            centerPoint = targetTransform.position;
            distance = Vector3.Distance(transform.position, targetPoints[currentVector]);
            MoveAroundCircle();
        }
    }

    private void MoveAroundCircle()
    {
        if (!isWaiting)
        {
            Vector3 targetPoint = targetPoints[currentVector];
            Vector3 direction = (targetPoint - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            StartCoroutine(WaitForDistance(targetPoint));
            
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, transform.up);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * torqueSpeed);
            rb.MoveRotation(rotation);
            rb.transform.rotation = rotation;
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