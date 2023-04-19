using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class CollectFood : AntAIState
{
    private LittleGuy littleGuy;
    private OscarVision vision;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        littleGuy = aGameObject.GetComponent<LittleGuy>();
        
        vision = aGameObject.GetComponentInChildren<OscarVision>();
    }

    public override void Enter()
    {
        base.Enter();
        
        littleGuy.GetComponentInChildren<ColourChangeShader>().attackPhase = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (vision.foodInSight.Count > 0)
        {
            littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
                vision.foodInSight[0].transform.position - transform.position, Vector3.up) * littleGuy.turnSpeed,0);
            littleGuy.rb.AddRelativeForce(Vector3.forward * (littleGuy.speed * 4), ForceMode.Acceleration);
        }
        else
        {
            Finish();
        }
    }
}
