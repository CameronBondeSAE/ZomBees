using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour
{
    public float tetherForceMag;
    private Vector3 homePoint;
    private Rigidbody rb;
    
    public void StartTether(Rigidbody newRb, Vector3 newHomePoint)
    {
        rb = newRb;
        homePoint = newHomePoint;
        Vector3 forceDirection = (homePoint - transform.position).normalized;
        rb.AddForce(forceDirection * tetherForceMag, ForceMode.Force);
    }
}