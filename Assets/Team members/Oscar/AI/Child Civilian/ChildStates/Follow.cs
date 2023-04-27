using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class Follow : OscarsLittleGuyMovement
{
    private OscarVision vision;
    private GameObject targetPos;
    private float maxFollowingDistance;
    private float distance;
    private float targetSpeed = 10;
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        vision = aGameObject.GetComponentInChildren<OscarVision>();
    }

    public override void Enter()
    {
        base.Enter();
        if (vision.civsInSight.Count > 0)
        {
            targetPos = vision.civsInSight[0].gameObject;
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (childControl.AmIFollowing == true)
        {
            maxFollowingDistance = 2f;
            distance = Vector3.Distance(targetPos.transform.position, transform.position);
            
            if (distance > maxFollowingDistance)
            {
                BasicMovement(targetSpeed);
                TurnTowards(targetPos.transform.position);
            }
            else
            {
                BasicStopping();
            }
        }
        else
        {
            Finish();
        }
    }
}
