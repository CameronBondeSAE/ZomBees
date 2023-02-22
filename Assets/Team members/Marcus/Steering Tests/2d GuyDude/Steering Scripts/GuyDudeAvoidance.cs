using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
    public class GuyDudeAvoidance : MonoBehaviour
    {
        private Rigidbody rb;

        public GuyDudeFeelers leftFeeler;
        public GuyDudeFeelers rightFeeler;
        public GuyDudeBail bailFeeler;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
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
            rb.AddTorque(Vector3.up * desiredTurnForce);
        }
    }
}
