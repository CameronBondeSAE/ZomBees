using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class WanderState : AntAIState
    {
        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<AdvancedGuyDudeMovement>().MoveToPoint
                (PatrolManager.singleton.pathsWithIndoors[Random.Range(0,PatrolManager.singleton.pathsWithIndoors.Count)]);
        }
    }
}
