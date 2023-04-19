using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class AttackCivilian : AntAIState
{
    private LittleGuy littleGuy;
    private OscarVision vision;

    private GameObject target;
    
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
        
        if (vision.civsInSight.Count >= 1)
        {
            littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
                vision.civsInSight[0].transform.position - transform.position, Vector3.up) * littleGuy.turnSpeed,0);
            littleGuy.rb.AddRelativeForce(Vector3.forward * (littleGuy.speed * 2), ForceMode.Acceleration);
        }
        else
        {
            Finish();
        }
    }
}
