using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Oscar
{
    public class TurnTowards : Oscar.StateBase
    {
        public LittleGuy guy;
        public GameObject target;
        private Transform targetTransform;
        private Vector3 targetPos;
        
        private RaycastHit hitInfo;
        private float distance = 10f;
        private int direction = 1;
        public float turnSpeed;

        private void Start()
        {
            targetTransform = target.transform;
        }

        void Update()
        {
            if (target)
            {
                targetPos = targetTransform.position;
            }

            Vector3 targetDir = targetPos - transform.position;

            float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
            
            guy.rb.AddRelativeTorque(0,angle * turnSpeed,0);
        }
        
        #region stateRegions

        public override void Enter()
        {
            Debug.Log("AVOID IT!");
            base.Enter();
        }
        
        public override void Execute()
        {
            Debug.Log("Execute");
            base.Execute();
        }
        
        public override void Exit()
        {
            Debug.Log("Exit");
            base.Exit();
        }

        #endregion
    }
}

