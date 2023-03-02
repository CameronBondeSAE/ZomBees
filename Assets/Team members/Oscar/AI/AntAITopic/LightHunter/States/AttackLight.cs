using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class AttackLight : AntAIState
{
    private LittleGuy littleGuy;
    private LightVisionAI vision;

    private GameObject target;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        littleGuy = aGameObject.GetComponent<LittleGuy>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (vision.lightInSight[0] != null)
        {
            target = vision.lightInSight[0];
        }

        Vector3 targetDir = target.transform.position - transform.position;

        float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
            
        littleGuy.rb.AddRelativeTorque(0,angle * littleGuy.speed,0);

        littleGuy.rb.AddRelativeForce(Vector3.forward * (littleGuy.speed * 2),ForceMode.Acceleration);

    }
}
