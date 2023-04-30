using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Lloyd;
using Random = UnityEngine.Random;

public class RunAway : OscarsLittleGuyMovement
{
    //forloop through a list of patrol points and go to the closest one with navmesh.
    //investigate navmesh for points to run to.
    
    private Vector3 targetPos;

    private void OnEnable()
    {
        objectArrivedEvent += LocationArrivedAt;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        targetPos = PatrolManager.singleton
                .pathsWithIndoors[Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count)].transform.position;
        
        NavmeshFindLocation(targetPos);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        NavmeshToLocation();
    }

    private void LocationArrivedAt()
    {
        if (childControl != null)
        {
            childControl.ShouldIHide = true;
            childControl.AmIScared = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        NavMeshFinish();
    }
}
