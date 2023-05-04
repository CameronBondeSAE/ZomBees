using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using Oscar;
using UnityEngine;
using Memory = Marcus.Memory;

public class FindStuff : OscarsLittleGuyMovement
{
    private ChildCivController childControl;
    
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        childControl = aGameObject.GetComponent<ChildCivController>();
    }

    public override void Enter()
    {
        base.Enter();
        
        NavmeshEnabled();        
        NavmeshFindLocation(childControl.goToPos);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        NavmeshToLocation();
    }
    
    public override void Exit()
    {
        base.Exit();
            
        NavMeshFinish();
    }
}
