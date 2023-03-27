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

        private void OnEnable()
        {
            foodSense = GetComponentInParent<TurnToFood>();
        }

        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<AdvancedGuyDudeMovement>().MoveToPoint
                (PatrolManager.singleton.resourcePoints[Random.Range(0,PatrolManager.singleton.resourcePoints.Count)]);
            
            foodSense.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            foodSense.gameObject.SetActive(false);
        }
    }
}
