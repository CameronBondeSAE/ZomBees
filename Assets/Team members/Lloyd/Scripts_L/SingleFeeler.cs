using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class SingleFeeler : MonoBehaviour
{
    public float sightDistance;

    public LayerMask layerMask;

    public int myInt;

    public TurnTowards turnTowards;

    private void Start()
    {
        turnTowards = GetComponentInParent<TurnTowards>();
    }

    private void FixedUpdate()
    {
        var transformForward = transform.forward * sightDistance;
        Debug.DrawRay(transform.position, transformForward, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transformForward, out hit, sightDistance, layerMask))
        {
            
            if (myInt == 1)
            {
                Debug.Log("left hit");
                Debug.DrawRay(transform.position, transformForward, Color.red);
                turnTowards.ApplyTorque(25);
            }
            else        {
                Debug.Log("right hit");
                Debug.DrawRay(transform.position, transformForward, Color.magenta);
                turnTowards.ApplyTorque(-25);
            }
        }
        else
        {
            turnTowards.FreezeTorque();
        }
    }
}