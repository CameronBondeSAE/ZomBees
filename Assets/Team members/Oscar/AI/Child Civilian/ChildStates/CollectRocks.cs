using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class CollectRocks : OscarsLittleGuyMovement
{
    private OscarVision vision;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        vision = aGameObject.GetComponentInChildren<OscarVision>();
    }
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (vision.objectsInSight.Count > 0)
        {
            TurnTowards(vision.objectsInSight[0].transform.position);
            
            BasicMovement(4f);
        }
        else
        {
            Finish();
        }
    }
}
