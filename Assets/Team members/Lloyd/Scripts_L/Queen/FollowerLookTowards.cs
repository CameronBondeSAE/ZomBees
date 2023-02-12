using System;
using UnityEngine;
using Lloyd;

public class FollowerLookTowards : MonoBehaviour
{
    public Transform target;
    public float torqueSpeed;
    public Rigidbody rb;

    public void SetTarget(Transform transform)
    {
        target = transform;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetDir = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * torqueSpeed);
        rb.MoveRotation(rotation);
    }
}