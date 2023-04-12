using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FlyDudeSeparation : MonoBehaviour
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
            targetPosition = CalculateMove();
            
            Vector3 directionAwayFromTarget = (transform.position - targetPosition).normalized;
            rb.AddForce(directionAwayFromTarget * force);
        }

        public Vector3 CalculateMove()
        {
            if (neighbours.neighbourDudes.Count == 0)
            {
                return Vector3.zero; 
            }

            Vector3 separationMove = Vector3.zero;
            
            int neighbourDudesCount = neighbours.neighbourDudes.Count;
            if (neighbourDudesCount > 5)
            {
                neighbourDudesCount = 5; // CAM HACK: Limit neighbours when there's loads
            }

            for (int index = 0; index < neighbourDudesCount; index++)
            {
                separationMove += neighbours.neighbourDudes[index].position;
            }

            separationMove /= neighbours.neighbourDudes.Count;
            return separationMove;
        }
    }
}
