using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class Idle : OscarsLittleGuyMovement
{
    private float elapsedTime;

    private ChildCivController childControl;

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
        
        if (elapsedTime <= 10)
        {
            //wait for player to finish
        }
        else
        {
            childControl.AmIIdle = false;
            Finish();        
        }
    }
}
