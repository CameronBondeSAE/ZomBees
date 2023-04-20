using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class CollectHoney : OscarsLittleGuyMovement
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
        
        if (vision.foodInSight.Count > 0)
        {
            TurnTowards(vision.foodInSight[0].transform.position);
            
            BasicMovement(2f);
        }
        else
        {
            Finish();
        }
    }
}
