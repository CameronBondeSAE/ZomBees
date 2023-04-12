using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class GetFood : AntAIState
    {
        private Rigidbody rb;
        
        public override void Enter()
        {
            base.Enter();

            GetComponentInParent<GuyDudeAvoidance>().enabled = false;
            rb = GetComponentInParent<Rigidbody>();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            rb.AddRelativeForce(Vector3.forward);
        }

        public override void Exit()
        {
            base.Exit();

            GetComponentInParent<GuyDudeAvoidance>().enabled = true;
        }
    }
}
