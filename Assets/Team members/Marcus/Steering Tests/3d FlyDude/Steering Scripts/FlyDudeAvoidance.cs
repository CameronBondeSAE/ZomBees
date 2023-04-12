using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FlyDudeAvoidance : MonoBehaviour
    {
        private Rigidbody rb;
        private float turnSpeed;

        public FlyDudeFeelers leftFeeler;
        public FlyDudeFeelers rightFeeler;
        public FlyDudeBail bailFeeler;

        private void Start()
        {
            turnSpeed = GetComponent<FlyDudeStats>().speed;
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float bailForce = bailFeeler.CalculateForce();
            float leftForce = leftFeeler.TurnForce();
            float rightForce = rightFeeler.TurnForce() * -1;

            if(bailForce > 0)
            {
                TurnAway(bailForce);
            }
            else
            {
                TurnAway(leftForce + rightForce);
            }
        }

        void TurnAway(float desiredTurnForce)
        {
            rb.AddTorque(transform.up * (desiredTurnForce * turnSpeed));
        }
    }
}
