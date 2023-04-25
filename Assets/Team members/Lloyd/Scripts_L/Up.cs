using System;
using System.Collections;
using System.Collections.Generic;
using Team_members.Lloyd.BeeWings;
using UnityEngine;

public class Up : MonoBehaviour
{
    private Rigidbody rb;

    private BeeWingsManager wings;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        wings = GetComponentInChildren<BeeWingsManager>();
        wings.SetWings();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.up);
    }
}
