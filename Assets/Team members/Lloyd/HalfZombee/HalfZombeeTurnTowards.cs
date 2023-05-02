using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

public class HalfZombeeTurnTowards : MonoBehaviour
{
    public Vector3 targetPosition;

    public Rigidbody rb;

    public float minAngle;

    public bool turning;

    public float torqueSpeed;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        turning = true;
    }

    public void Update()
    {
        if (turning)
        {
            Vector3 targetDir = targetPosition - transform.position;

            float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
            
            float torqueSign = Mathf.Sign(angle);

            if (angle < minAngle)
            {
                //can't strafe with MoveForwards on
                /*
                if (facingAway)
                    angle = -angle;*/

                rb.AddRelativeTorque(new Vector3(0, torqueSign * torqueSpeed, 0), ForceMode.VelocityChange);
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
