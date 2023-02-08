using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Move : MonoBehaviour
{
    public LittleGuy guy;
    public float speed;
    void Update()
    {
        guy.rb.AddRelativeForce(Vector3.forward * speed,ForceMode.Acceleration);
    }
}
