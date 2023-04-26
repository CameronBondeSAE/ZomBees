using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class Hide : OscarsLittleGuyMovement
    {
        private float elapsedTime;
        
        public override void Enter()
        {
            base.Enter();

            elapsedTime = 0f;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            elapsedTime += Time.deltaTime;
        
            if (elapsedTime <= 5)
            {
                //wait little scaredycat!
            }
            else
            {
                childControl.iAmScared = false;
                Finish();        
            }
        }
    }
}
