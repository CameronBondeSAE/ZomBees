using System;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Sirenix.OdinInspector;
using Team_members.Lloyd.Civilian_L;

public class LookAtTarget : MonoBehaviour
{
    public Transform transformTarget;

    public Vector3 vectorTarget;

    Vector3 targetDir;

    public float torqueSpeed;
    public Rigidbody rb;

    public bool lookingAtPoint;
    public bool lookingAtTransform;

    public bool looking=false;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    [Button]
    public void SetVectorTarget(Vector3 vecTarget)
    {
        looking = false;
        vectorTarget = vecTarget;
        lookingAtTransform = false;
        lookingAtPoint = true;
        looking = true;
    }

    [Button]
    public void SetTarget(Transform newTarget)
    {
        looking = false;
        transformTarget = newTarget;
        lookingAtTransform = true;
        lookingAtPoint = false;
        looking = true;
    }

    private void FixedUpdate()
    {
        if (looking)
        {
            if (lookingAtTransform)
            {
                targetDir = transformTarget.position - transform.position;
            }
            else if (lookingAtPoint)
            {
                targetDir = vectorTarget - transform.position;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDir, transform.up);
            Quaternion rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * torqueSpeed);
            
            rb.MoveRotation(rotation);
            rb.transform.rotation = rotation;
        }
    }
}