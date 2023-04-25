using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class AttackCivilian : OscarsLittleGuyMovement
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
        
        if (vision.civsInSight.Count >= 1)
        {
            TurnTowards(vision.civsInSight[0].transform.position);
            
            BasicMovement(2f);
        }
        else
        {
            Finish();
        }
    }
}
