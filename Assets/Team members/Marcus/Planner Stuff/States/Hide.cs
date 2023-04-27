using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class Hide : AntAIState
    {
        public override void Enter()
        {
            base.Enter();
            //Find nearest hiding spot
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            //Run towards and hide at our desired spot
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
