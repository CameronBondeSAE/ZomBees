using System;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Team_members.Lloyd.Civilian_L;

public class LookTowards : CivModelAIState
{
    public Transform target;
    public float torqueSpeed;
    public Rigidbody rb;

    public bool looking;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

       // WaitUntil (civBrain.target)
       // target = civBrain.target;
    }

    private void FixedUpdate()
    {
        if (looking)
        {
            Vector3 targetDir = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir, transform.up);
            Quaternion rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * torqueSpeed);
            rb.MoveRotation(rotation);
        }
    }
}