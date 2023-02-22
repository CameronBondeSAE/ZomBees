using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class HuntCivilianState : AntAIState
{
    public LittleGuy guy;
    public GameObject target;
    private Transform targetTransform;
    private Vector3 targetPos;

    public float turnSpeed;
    

    public override void Enter()
    {
        base.Enter();
        
        if (target != null)
        {
            targetTransform = target.transform;
        }
        else
        {
            Finish();
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (target)
        {
            targetPos = targetTransform.position;
        }

        Vector3 targetDir = targetPos - transform.position;

        float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
            
        guy.rb.AddRelativeTorque(0,angle * turnSpeed,0);
        
    }
}
