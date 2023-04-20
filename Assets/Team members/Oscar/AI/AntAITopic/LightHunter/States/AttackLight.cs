using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class AttackLight : OscarsLittleGuyMovement
{
    private OscarVision vision;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        vision = aGameObject.GetComponent<OscarVision>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (vision.lightInSight.Count > 0)
        {
            TurnTowards(vision.lightInSight[0].transform.position);
            
            BasicMovement(4f);
        }
        else
        {
            Finish();
        }

    }
}
