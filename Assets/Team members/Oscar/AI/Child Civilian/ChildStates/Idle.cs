using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class Idle : AntAIState
{
    private LittleGuy littleGuy;
    private OscarVision vision;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        littleGuy = aGameObject.GetComponent<LittleGuy>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
            new Vector3(0, Time.deltaTime, 0), Vector3.up) * littleGuy.turnSpeed,0);
    }
}
