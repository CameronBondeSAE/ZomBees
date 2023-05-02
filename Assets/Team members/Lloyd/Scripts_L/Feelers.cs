using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class Feelers : MonoBehaviour
{
    public int numRaycasts;
    private float raycastAngleVal;
    public float raycastAngle;
    public float sightDistance;
    public LayerMask layerMask;

    public float force;
    
    public float[] customSightDistances;

    public float multiplier;

   // public TurnTowards turnTowards;

    private void Start()
    {
      //  turnTowards = GetComponent<TurnTowards>();
    }

    private void FixedUpdate()
    {
        raycastAngleVal = raycastAngle / numRaycasts;
        for (int i = 0; i < numRaycasts; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(i * raycastAngleVal, Vector3.up) * transform.forward;
            Debug.DrawRay(transform.position, direction * GetSightDistance(i), Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, GetSightDistance(i), layerMask))
            {
                float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
                float torque = angle * force * multiplier;
               // turnTowards.ApplyTorque(torque);
            }
        }
    }

    private float GetSightDistance(int rayIndex)
    {
        if (rayIndex < numRaycasts / 3)
        {
            return customSightDistances[0];
        }
        else if (rayIndex >= numRaycasts / 3 && rayIndex < (numRaycasts * 2) / 3)
        {
            return customSightDistances[1];
        }
        else
        {
            return customSightDistances[2];
        }
    }
    
}
