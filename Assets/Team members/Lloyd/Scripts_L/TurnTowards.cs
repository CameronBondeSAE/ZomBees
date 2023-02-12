using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lloyd
{ 
    public class TurnTowards : MonoBehaviour
    {
        private Rigidbody rb;

        private Vector3 targetPosition;

        public float targetDist;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;


            targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + targetDist);
        }

        public void ApplyTorque(float torque)
        {
        //     Vector3 targetDir = targetPosition - transform.position;
        //     float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        Vector3 angularVelocity = rb.angularVelocity;
        angularVelocity.x = 0;
        angularVelocity.z = 0;
        rb.angularVelocity = angularVelocity;

        rb.AddTorque(transform.position * torque);
        }

        public void FreezeTorque()
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}