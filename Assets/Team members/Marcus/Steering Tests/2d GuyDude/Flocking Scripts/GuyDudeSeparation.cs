using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class GuyDudeSeparation : MonoBehaviour
    {
        public GuyDudeNeighbours neighbours;
        
        Vector3 targetPosition;
        private Rigidbody rb;
        public float force;
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            targetPosition = CalculateMove(neighbours.neighbourDudes);
            
            Vector3 directionAwayFromTarget = (transform.position - targetPosition).normalized;
            rb.AddForce(directionAwayFromTarget * force);
        }

        public Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return Vector3.zero; 
            }

            Vector3 separationMove = Vector3.zero;
            
            foreach (Transform item in neighbours)
            {
                separationMove += item.position;
            }

            separationMove /= neighbours.Count;
            return separationMove;
        }
    }
}
