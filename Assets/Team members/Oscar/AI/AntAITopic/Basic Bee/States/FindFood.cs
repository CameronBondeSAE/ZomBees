using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Oscar;
using Random = UnityEngine.Random;

public class FindFood : OscarsLittleGuyMovement
{
    private Vector3 patrolPointLoc;

    /*private static PatrolManager patrolManager;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        patrolManager = aGameObject.GetComponent<PatrolManager>();
    }*/

    public override void Enter()
    {
        base.Enter();

        //patrolPointLoc = PatrolManager.singleton.resourcePoints[Random.Range(0, PatrolManager.singleton.resourcePoints.Count)].transform.position;

        //NavmeshEnabled();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
            
        //TODO Ask cam why this is not an active member of navmesh when it has the navmesh agent on it.
        //NavmeshFindLocation(patrolPointLoc);
        BasicMovement(1f);
    }

    public override void Exit()
    {
        base.Exit();
            
        //NavMeshFinish();
    }
}
