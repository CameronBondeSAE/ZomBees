using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Marcus
{
    public class AdvancedGuyDudeMovement : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        private float arrivalDisance = 1.5f;
        
        public PatrolPoint targetPoint;

        public void MoveToPoint(PatrolPoint destination)
        {
            targetPoint = destination;
            navMeshAgent.SetDestination(targetPoint.transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if (ReachedDestinationOrGaveUp() && targetPoint)
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

        #region Manual Movement Test

        private class FakeScript : TurnFunction
        {
            private NavMeshPath path;
            public PatrolPoint targetPoint;
            public Rigidbody rb;
            public float speed;

            private int pathCounter;

            private void MyFakeStart()
            {
                path = new NavMeshPath();
            }

            private void FakeMoveToPoint(PatrolPoint destination)
            {
                targetPoint = destination;
                pathCounter = 0;
            }
        
            private void MyFakeUpdate()
            {
                NavMesh.CalculatePath(transform.position, targetPoint.transform.position, NavMesh.AllAreas, path);
                var nextPoint = path.corners[pathCounter];

                if (targetPoint)
                {
                    if (transform.forward + nextPoint.normalized != nextPoint)
                    {
                        TurnTowards(rb, nextPoint, 2f);
                    }
                    else
                    {
                        rb.AddRelativeForce(Vector3.forward * speed);
                    }
                }
                
                // Just for debug
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                }
            }
        }

        #endregion
    }
}
