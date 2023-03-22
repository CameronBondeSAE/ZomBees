using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class Spin : AntAIState
    {
        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<GuyDudeMovement>().MoveToPoint
                (PatrolManager.singleton.indoors[Random.Range(0,PatrolManager.singleton.indoors.Count)]);
        }
    }
}
