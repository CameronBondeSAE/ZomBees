using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FlyDudeCohesion : MonoBehaviour
    {
        public FlyDudeNeighbours neighbours;
        
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

            Vector3 directionTowardsTarget = (targetPosition - transform.position).normalized;
            rb.AddForce(directionTowardsTarget * force);
        }

        public Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return Vector3.zero; 
            }

            Vector3 cohesionMove = Vector3.zero;
            
            foreach (Transform item in neighbours)
            {
                cohesionMove += item.position;
            }

            cohesionMove /= neighbours.Count;
            return cohesionMove;
        }
    }
}
