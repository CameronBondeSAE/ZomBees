using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Marcus
{
    public class AdvancedGuyDudeMovement : TurnFunction
    {
        public bool usingRigidbodyMovement;
        
        public Rigidbody rb;
        public float speed;
        public float stoppingDistance;
        public PatrolPoint targetPoint;

        public NavMeshAgent navMeshAgent;
        
        private NavMeshPath path;
        [ReadOnly] [SerializeField]
        private int pathCounter;

        private void OnEnable()
        {
            path = new NavMeshPath();

            if (usingRigidbodyMovement)
            {
                rb.isKinematic = false;
                navMeshAgent.enabled = false;
            }
            else
            {
                rb.isKinematic = true;
                navMeshAgent.enabled = true;
            }
        }

        public void MoveToPoint(PatrolPoint destination)
        {
            if (usingRigidbodyMovement)
            {
                pathCounter = 0;
            }
            else
            {
                navMeshAgent.SetDestination(destination.transform.position);
            }
            
            targetPoint = destination;
            NavMesh.CalculatePath(transform.position, targetPoint.transform.position, NavMesh.AllAreas, path);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (usingRigidbodyMovement)
            {
                if (targetPoint)
                {
                    Vector3 nextPoint = path.corners[pathCounter];
                    float distanceFromPoint = Vector3.Distance(transform.position, nextPoint);
                
                    if (distanceFromPoint <= stoppingDistance)
                    {
                        pathCounter++;
                        if (pathCounter >= path.corners.Length)
                        {
                            targetPoint = null;
                        }
                    }
                
                    float turnSpeed = Vector3.Angle(transform.forward, nextPoint)/3f;
                    TurnTowards(rb, nextPoint, turnSpeed);
                
                    rb.AddRelativeForce(Vector3.forward * speed);
                }
            }
            else
            {
                if (ReachedDestinationOrGaveUp())
                {
                    targetPoint = null;    
                }
                
            }
            
            for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
        
        public bool ReachedDestinationOrGaveUp()
        {

            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + stoppingDistance)
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
