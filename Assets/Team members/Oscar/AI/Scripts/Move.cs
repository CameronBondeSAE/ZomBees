using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Move : MonoBehaviour
    {
        public float speedIncreaseMultiplyer;
        public LittleGuy littleGuy;
        void Update()
        {
            float decidedSpeed = littleGuy.speed * speedIncreaseMultiplyer;
            littleGuy.rb.AddRelativeForce(Vector3.forward * decidedSpeed,ForceMode.Acceleration);
        }
    }
}

