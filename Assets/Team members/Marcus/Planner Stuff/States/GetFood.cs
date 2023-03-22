using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class GetFood : AntAIState
    {
        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<GuyDudeAvoidance>().enabled = false;
        }

        public override void Exit()
        {
            base.Exit();

            GetComponentInParent<GuyDudeAvoidance>().enabled = true;
        }
    }
}
