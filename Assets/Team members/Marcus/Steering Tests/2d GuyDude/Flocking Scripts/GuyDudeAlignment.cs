using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class GuyDudeAlignment : MonoBehaviour
    {
        // Variable pointing to your Neighbours component
        public GuyDudeNeighbours neighbours;
        
        public float force;
        Vector3 targetDirection;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Some are Torque, some are Force		
            targetDirection = CalculateMove(neighbours.neighbourDudes);

            // Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector
            Vector3 cross = Vector3.Cross(transform.forward, targetDirection);

            rb.AddTorque(cross * force);
        }

        public Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return Vector3.zero; 
            }

            Vector3 alignmentMove = Vector3.zero;

            // Average of all neighbours directions
            // I’m using a list of transforms in my neighbours script, you might be using GameObjects etc
            foreach (Transform item in neighbours)
            {
                alignmentMove += item.transform.forward;
            }

            alignmentMove /= neighbours.Count;
            return alignmentMove;
        }
    }
}
