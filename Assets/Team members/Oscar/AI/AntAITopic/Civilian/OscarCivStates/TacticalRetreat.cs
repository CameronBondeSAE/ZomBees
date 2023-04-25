using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Lloyd;

public class TacticalRetreat : OscarsLittleGuyMovement
{
    private Hearing ears;
    
    private Vector3 targetPos;
    float elapsedTime;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ears = aGameObject.GetComponent<Hearing>();
    }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = 0f;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (ears.heardSound)
        {
            targetPos = ears.loudestRecentSound.Source.transform.position;
        }

        elapsedTime += Time.deltaTime;
        
        if (elapsedTime <= 5)
        {
            TurnAway(targetPos);
                    
            BasicMovement(3f);        
        }
        else
        {
            Finish();        
        }
    }
}
