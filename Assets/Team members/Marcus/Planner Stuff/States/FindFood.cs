using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
    public class FindFood : AntAIState
    {
        public Rigidbody rb;
        public GameObject foodSense;
        
        private float turnSpeed;
        
        public float walkTime;
        private float directionTimer;

        public override void Enter()
        {
            base.Enter();

            foodSense.SetActive(true);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            directionTimer -= aDeltaTime;

            if (directionTimer <= 0)
            {
                turnSpeed = Random.Range(5f, 25f);
                rb.AddTorque(Vector3.up * turnSpeed);

                directionTimer = walkTime;
            }
        }

        public override void Exit()
        {
            base.Exit();

            foodSense.SetActive(false);
        }
    }
}
