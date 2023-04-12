using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FlyDudeAlignment : MonoBehaviour
    {
        // Variable pointing to your Neighbours component
        public FlyDudeNeighbours neighbours;
        
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
            targetDirection = CalculateMove();

            // Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector
            Vector3 cross = Vector3.Cross(transform.forward, targetDirection);

            rb.AddTorque(cross * force);
        }

        public Vector3 CalculateMove()
        {
            if (neighbours.neighbourDudes.Count == 0)
            {
                return Vector3.zero; 
            }

            Vector3 alignmentMove = Vector3.zero;

            // Average of all neighbours directions
            // I’m using a list of transforms in my neighbours script, you might be using GameObjects etc
            int neighbourDudesCount = neighbours.neighbourDudes.Count;
            if (neighbourDudesCount > 5)
            {
                neighbourDudesCount = 5; // CAM HACK: Limit neighbours when there's loads
            }
            
            for (int index = 0; index < neighbourDudesCount; index++)
            {
                alignmentMove += neighbours.neighbourDudes[index].forward;
            }

            alignmentMove /= neighbours.neighbourDudes.Count;
            return alignmentMove;
        }
    }
}
