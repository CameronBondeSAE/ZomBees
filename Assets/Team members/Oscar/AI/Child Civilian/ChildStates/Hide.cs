using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class Hide : OscarsLittleGuyMovement
    {
        private ChildCivController childControl;
        
        private float elapsedTime;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            childControl = aGameObject.GetComponent<ChildCivController>();
        }

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
                childControl.AmIScared = false;
                childControl.ShouldIHide = false;
                Finish();        
            }
        }
    }
}
