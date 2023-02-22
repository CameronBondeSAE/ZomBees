using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Alignment : MonoBehaviour
    {
        public Oscar.LittleGuy guy;
        public Oscar.Neighbours neighbours;

        public float force;

        // Variable pointing to your Neighbours component
        // Neighbours neighbours;

        void FixedUpdate()
        {
            // Some are Torque, some are Force		
            Vector3 targetDirection = CalculateMove(neighbours.friendsList);
		
            // Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector
            Vector3 cross = Vector3.Cross(transform.forward, targetDirection);

            guy.rb.AddTorque(cross * force);
        }

        public Vector3 CalculateMove(List<Transform> FriendsPos)
        {
            if (FriendsPos.Count == 0)
                return Vector3.zero;

            Vector3 alignmentMove = Vector3.zero;

            // Average of all neighbours directions
            // I’m using a list of transforms in my neighbours script, you might be using GameObjects etc
            foreach (Transform item in FriendsPos)
            {
                alignmentMove += item.transform.forward;
            }

            alignmentMove /= FriendsPos.Count;

            return alignmentMove;
        }
       
    }
}
