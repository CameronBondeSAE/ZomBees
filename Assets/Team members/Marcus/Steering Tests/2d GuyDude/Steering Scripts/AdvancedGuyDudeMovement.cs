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
        public Rigidbody rb;
        public float speed;
        public float stoppingDistance;
        public PatrolPoint targetPoint;
        
        private NavMeshPath path;
        [ReadOnly] [SerializeField]
        private int pathCounter;

        private void OnEnable()
        {
            path = new NavMeshPath();
        }

        public void MoveToPoint(PatrolPoint destination)
        {
            targetPoint = destination;
            pathCounter = 0;
            
            path.ClearCorners();
            NavMesh.CalculatePath(transform.position, targetPoint.transform.position, NavMesh.AllAreas, path);
        }

        // Update is called once per frame
        void Update()
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

                float turnSpeed = Vector3.Angle(transform.forward, nextPoint);
                TurnTowards(rb, nextPoint, turnSpeed);
                
                rb.AddRelativeForce(Vector3.forward * speed);
            }
            
            /*for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);*/
        }
    }
}
