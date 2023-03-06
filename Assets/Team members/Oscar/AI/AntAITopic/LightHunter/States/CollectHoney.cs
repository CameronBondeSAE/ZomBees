using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class CollectHoney : AntAIState
{
    private LittleGuy littleGuy;
    private LightVisionAI vision;
    
    private GameObject target;
    private Vector3 targetDir;
    private float angle;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
                
        littleGuy = aGameObject.GetComponent<LittleGuy>();

        vision = aGameObject.GetComponent<LightVisionAI>();

        // target = vision.honeyInSight[0];
        // targetDir = target.transform.position - transform.position;
        // float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        /* angle = Vector3.SignedAngle(transform.forward,
                   vision.honeyInSight[0].transform.position - transform.position, Vector3.up);*/
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        //littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, angle * littleGuy.speed,0);

        if (vision.honeyInSight.Count > 0)
        {
            littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, vision.honeyInSight[0].transform.position - transform.position, Vector3.up) * littleGuy.speed,0);
            littleGuy.rb.AddRelativeForce(Vector3.forward * (littleGuy.speed * 2), ForceMode.Acceleration);
        }
        else
        {
            Finish();
        }
    }
}
