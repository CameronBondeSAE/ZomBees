using System;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Team_members.Lloyd.Civilian_L;

public class LookTowards : MonoBehaviour
{
    private CivilianBrain civBrain;

    public Transform target;
    public float torqueSpeed;
    public Rigidbody rb;

    public bool looking;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

        civBrain = GetComponent<CivilianBrain>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {
        if (civBrain != null)
        {
            looking = civBrain.looking;
            target = civBrain.target;
        }

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