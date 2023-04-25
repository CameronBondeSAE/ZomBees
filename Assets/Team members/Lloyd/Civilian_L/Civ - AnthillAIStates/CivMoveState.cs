using System;
using System.Collections;
using UnityEngine;

namespace Lloyd
{
    public class CivMoveState : CivModelAIState
    {
        public Transform target;
        
        //get from stats
        public float moveSpeed = 25f;
        public float stopDistance = 1f;
        public float decelerationDistance;
        
        public bool inRange = false;

        public override void Enter()
        {
            base.Enter();
            decelerationDistance = stopDistance * 3;

            civBrain.inRange = inRange;
        }

        private void FixedUpdate()
        {
            target = civBrain.target;

            if (target == null) return;

            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= stopDistance)
            {
                Stop();
            }
            
            else if (distance <= decelerationDistance)
            {
                float decelerationFactor = Mathf.Clamp01((distance - stopDistance) / (decelerationDistance - stopDistance));
                rb.AddForce(direction.normalized * moveSpeed * decelerationFactor, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(direction.normalized * moveSpeed, ForceMode.Acceleration);
            }
        }

        private void Stop()
        {
            rb.velocity = Vector3.zero;
            inRange = true;
            civBrain.inRange = inRange;
            Finish();
        }

        public override void Exit()
        {
            base.Exit();
            inRange = false; 
        }
    }
}