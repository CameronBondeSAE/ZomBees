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

    private Vision vision;

    public override void Enter()
    {
        base.Enter();

        vision = GetComponent<Vision>();
        
        if (vision.civilGuyInSight != null)
        {
            
        }
        else
        {
            Reset();
        }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        // if (target)
        // {
        //     targetPos = targetTransform.position;
        // }
        //
        // Vector3 targetDir = targetPos - transform.position;
        //
        // float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        //     
        // guy.rb.AddRelativeTorque(0,angle * turnSpeed,0);
        // if (transform.position == targetPos)
        // {
        //     Finish();
        // }
        Finish();
    }
}
