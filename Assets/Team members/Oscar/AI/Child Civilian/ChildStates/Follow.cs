using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class Follow : AntAIState
{
    private LittleGuy littleGuy;
    private OscarVision vision;
    private ChildCivController childControl;

    private GameObject target;
    private float followDistance = 1f;
    float elapsedTime;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        littleGuy = aGameObject.GetComponent<LittleGuy>();
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
            littleGuy.rb.AddRelativeForce(Vector3.forward * (vision.civsInSight[0].GetComponent<LittleGuy>().speed), ForceMode.Acceleration);
            littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
                vision.civsInSight[0].transform.position - transform.position, Vector3.up) * littleGuy.turnSpeed,0);        }
        else
        {
            childControl.iAmFollowing = false;
            childControl.iAmScared = false;
            Finish();
        }
    }
}
