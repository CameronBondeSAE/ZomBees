using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyDudeWander : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float wander = Mathf.PerlinNoise(Time.time, 0);
        rb.AddRelativeTorque(0, wander, 0);
    }
}
