using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class Avoid : MonoBehaviour
{
    public Rigidbody rb;
    private float distance = 5f;

    private float turnTimer;
    private int turnDir;
    private float turnSpeedModif = 1f;

    // Update is called once per frame
    void Update()
    {
        turnTimer -= Time.deltaTime;
        if (turnTimer <= 0)
        {
            ChangeDir();
        }
        
        RaycastHit hitInfo;
        Physics.Raycast(rb.transform.localPosition, transform.forward, out hitInfo, distance, 255);

        if (hitInfo.collider)
        {
            turnSpeedModif += Random.Range(-0.5f, 0.5f);
            rb.AddRelativeTorque(Vector3.up * turnSpeedModif * turnDir);
        }
    }

    void ChangeDir()
    {
        // Generate either -1 or 1
        turnDir = Random.Range(0, 2) * 2 - 1;
        turnTimer = 1f;
    }
}
