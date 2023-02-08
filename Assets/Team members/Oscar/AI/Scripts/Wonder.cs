using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Wonder : MonoBehaviour
    {
        public LittleGuy guy;

        void Update()
        {
            guy.rb.AddRelativeForce(Vector3.forward * guy.speed,ForceMode.Acceleration);
        }
    }
}

