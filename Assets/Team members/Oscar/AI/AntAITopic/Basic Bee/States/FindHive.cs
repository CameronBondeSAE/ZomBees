using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class FindHive : AntAIState
{
    //I want the bee to scan the world and know where the Hive is to just deliver all its goods.
    
    //just turn towards for now then work on path finding.
    private LittleGuy littleGuy;
    public Hive hive;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        littleGuy = aGameObject.GetComponent<LittleGuy>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        littleGuy.rb.AddRelativeTorque(0,
            Vector3.SignedAngle(transform.forward, 
                littleGuy.myHive.transform.position - transform.position, Vector3.up) * littleGuy.turnSpeed, 0);
        littleGuy.rb.AddRelativeForce(Vector3.forward * (littleGuy.speed),ForceMode.Acceleration);
    }
}
