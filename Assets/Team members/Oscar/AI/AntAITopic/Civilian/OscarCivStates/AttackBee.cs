using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class AttackBee : OscarsLittleGuyMovement
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

        if (vision.beesInSight.Count > 0)
        {
            TurnTowards(vision.beesInSight[0].transform.position);
            
            BasicMovement(4f);
        }
        else
        {
            Finish();
        }
    }
}
