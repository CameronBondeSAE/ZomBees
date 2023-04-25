using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Lloyd;

public class RunAway : OscarsLittleGuyMovement
{
    //forloop through a list of patrol points and go to the closest one with navmesh.
    //investigate navmesh for points to run to.
    
    private Hearing ears;
    private OscarVision vision;
    private ChildCivController childControl;
    
    private Vector3 targetPos;
    float elapsedTime;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ears = aGameObject.GetComponent<Hearing>();
        vision = aGameObject.GetComponentInChildren<OscarVision>();
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
        
        
        if (ears.heardSound)
        {
            targetPos = ears.loudestRecentSound.Source.transform.position;
        }

        if (vision.beesInSight.Count >= 1)
        {
            targetPos = vision.beesInSight[0].transform.position;
        }
        
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime <= 5)
        {
            TurnAway(targetPos);
                    
            BasicMovement(5f);        
        }
        else
        {
            childControl.iAmScared = false;
            Finish();        
        }
    }
}
