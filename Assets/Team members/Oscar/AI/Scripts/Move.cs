using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Move : MonoBehaviour
    {
        public LittleGuy littleGuy;
        void Update()
        {
            littleGuy.rb.AddRelativeForce(Vector3.forward * littleGuy.speed,ForceMode.Acceleration);
        }
    }
}

