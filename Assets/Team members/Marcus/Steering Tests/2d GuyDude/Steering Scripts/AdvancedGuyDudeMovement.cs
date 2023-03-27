using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Marcus
{
    public class AdvancedGuyDudeMovement : TurnFunction
    {
        public Rigidbody rb;
        public float speed;
        public PatrolPoint targetPoint;
        
        private NavMeshPath path;
        private int pathCounter;
        
        private void Start()
        {
            path = new NavMeshPath();
        }

        public void MoveToPoint(PatrolPoint destination)
        {
            targetPoint = destination;
            pathCounter = 0;
                
            NavMesh.CalculatePath(transform.position, targetPoint.transform.position, NavMesh.AllAreas, path);
        }

        // Update is called once per frame
        void Update()
        {
            var nextPoint = path.corners[pathCounter];

            if (targetPoint)
            {
                Vector3 distanceFromPoint = targetPoint.transform.position - transform.position;
                
                if (transform.forward + distanceFromPoint != nextPoint)
                {
                    TurnTowards(rb, nextPoint, 2f);
                }
                else
                {
                    rb.AddRelativeForce(Vector3.forward * speed);
                }
            }
        }
    }
}
