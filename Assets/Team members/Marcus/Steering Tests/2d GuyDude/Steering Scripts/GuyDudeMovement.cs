using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Marcus
{
    public class GuyDudeMovement : MonoBehaviour
    {
        public float speed;
        private Rigidbody rb;

        public NavMeshAgent navMeshAgent;
        private float arrivalDisance = 1.5f;
        
        public PatrolPoint targetPoint;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void MoveToPoint(PatrolPoint destination)
        {
            targetPoint = destination;
            navMeshAgent.SetDestination(targetPoint.transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            // rb.AddRelativeForce(Vector3.forward * speed);
            
            if (ReachedDestinationOrGaveUp())
            {
                print("Got to my spot......");
                targetPoint = null;
            }
        }
        
        public bool ReachedDestinationOrGaveUp()
        {
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + arrivalDisance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
