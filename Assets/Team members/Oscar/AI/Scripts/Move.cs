using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Move : MonoBehaviour
    {
        public LittleGuy guy;
        void Update()
        {
            guy.rb.AddRelativeForce(Vector3.forward * guy.speed,ForceMode.Acceleration);
        }
    }
}

