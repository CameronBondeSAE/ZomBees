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
        public GameObject foodSense;

        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<GuyDudeMovement>().MoveToPoint
                (PatrolManager.singleton.resourcePoints[Random.Range(0,PatrolManager.singleton.resourcePoints.Count)]);
            
            foodSense.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            foodSense.SetActive(false);
        }
    }
}
