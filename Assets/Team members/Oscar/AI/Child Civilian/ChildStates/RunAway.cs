using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class RunAway : AntAIState
{
    //forloop through a list of patrol points and go to the closest one with navmesh.
    
    
    private HearingComp ears;
    private LittleGuy guy;
    private OscarVision vision;
    private ChildCivController childControl;
    
    private Vector3 runAwayFromPos;
    private Vector3 directionToRun;
    
    float elapsedTime;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        guy = aGameObject.GetComponent<LittleGuy>();
        ears = aGameObject.GetComponent<HearingComp>();
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
            runAwayFromPos = ears.loudestRecentSound;
            directionToRun = guy.transform.position - runAwayFromPos;
        }

        if (vision.beesInSight.Count >= 1)
        {
            runAwayFromPos = vision.beesInSight[0].transform.position;
            directionToRun = guy.transform.position - runAwayFromPos;
        }
        
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime <= 5)
        {
            guy.rb.AddRelativeTorque(0,Vector3.SignedAngle(guy.transform.forward,
                    directionToRun, Vector3.up),0);
                    
            guy.rb.AddRelativeForce(Vector3.forward * (guy.speed * 3), ForceMode.Acceleration);
        }
        else
        {
            childControl.iAmScared = false;
            Finish();        
        }
    }
}
