using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyDudeWandering : MonoBehaviour
{
    private Rigidbody rb;

    private float[] xOffset;
    private float[] zOffset;
    public float force;

    /// <summary>
    /// wander values ordered x,y,z
    /// </summary>
    private float[] wander;

    private void Awake()
    {
        xOffset = new float[3];
        zOffset = new float[3];
        wander = new float[3];
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            xOffset[i] = Random.Range(-1000, 1000);
            zOffset[i] = Random.Range(-1000, 1000);
        }
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            float x = xOffset[i] + Time.time;
            float z = zOffset[i] + Time.time;
        
            wander[i] = Mathf.PerlinNoise(x, z) * 2 - 1;
        }
        
        rb.AddRelativeTorque(wander[0] * force, 0, 0);
        rb.AddRelativeTorque(0, wander[1] * force, 0);
        rb.AddRelativeTorque(0, 0, wander[2] * (force/2));
    }
}
