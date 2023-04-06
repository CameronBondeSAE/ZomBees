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
        private TurnToFood foodSense;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            foodSense = aGameObject.GetComponentInChildren<TurnToFood>();
        }

        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<AdvancedGuyDudeMovement>().MoveToPoint
                (PatrolManager.singleton.resourcePoints[Random.Range(0,PatrolManager.singleton.resourcePoints.Count)]);

            foodSense.GetComponent<SphereCollider>().enabled = true;
        }

        public override void Exit()
        {
            base.Exit();

            foodSense.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
