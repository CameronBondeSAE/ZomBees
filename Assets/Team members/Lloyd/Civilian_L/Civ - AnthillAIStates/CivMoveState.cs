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

        private CivilianBrain civBrain;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            civBrain = gameObject.GetComponent<CivilianBrain>();
        }

        public override void Enter()
        {
            target = civBrain.target;

            moveSpeed = stats.moveSpeed;
            maxSpeed = stats.maxMoveSpeed;
            minDist = stats.minDist;

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Vector3 direction = target.position - rb.position;

            Vector3 force = direction * moveSpeed;

            rb.AddRelativeForce(force);
        }

    }
}