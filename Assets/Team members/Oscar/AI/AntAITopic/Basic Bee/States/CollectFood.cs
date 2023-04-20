using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class CollectFood : OscarsLittleGuyMovement
{
    private OscarVision vision;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        vision = aGameObject.GetComponentInChildren<OscarVision>();
    }

    public override void Enter()
    {
        base.Enter();
        
        //GetComponentInChildren<ColourChangeShader>().attackPhase = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (vision.foodInSight.Count > 0)
        {
            TurnTowards(vision.foodInSight[0].transform.position);
            
            BasicMovement(4f);
        }
        else
        {
            Finish();
        }
    }
}
