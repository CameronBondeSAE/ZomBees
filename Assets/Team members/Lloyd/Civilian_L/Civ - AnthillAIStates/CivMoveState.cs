using System;
using System.Collections;
using UnityEngine;

namespace Team_members.Lloyd.Civilian_L.Civ___AnthillAIStates
{
    public class CivMoveState : CivModelAIState
    {
        public bool moving = true;

        public Transform target;

        public float moveSpeed;

        public float maxSpeed;

        public float minDist;

        public override void Enter()
        {
            base.Enter();

            moveSpeed = stats.moveSpeed;
            maxSpeed = stats.maxMoveSpeed;
            minDist = stats.minDist;

            target = civBrain.target;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Vector3 direction = target.position - rb.position;

            Vector3 force = direction * moveSpeed;

            rb.AddRelativeForce(force);
        }

    }
}