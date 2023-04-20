using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class Follow : OscarsLittleGuyMovement
{
    private OscarVision vision;
    private ChildCivController childControl;

    float elapsedTime;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

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
        
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime <= 5)
        {
            if (vision.civsInSight.Count > 0)
            {
                BasicMovement(vision.civsInSight[0].GetComponent<LittleGuy>().speed);

                TurnTowards(vision.civsInSight[0].transform.position);
            }
        }
        else
        {
            childControl.iAmFollowing = false;
            childControl.iAmScared = false;
            Finish();
        }
    }
}
