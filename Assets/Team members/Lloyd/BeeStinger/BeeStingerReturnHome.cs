using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using UnityEngine;

public class BeeStingerReturnHome : AntAIState
{
    public BeeStingerSensor sensor;

    public ShaderGraphChangeColor shader;
    
    public Vector3 target;
    public Rigidbody rb;
    public float forceMultiplier;
    public float maxDistance;
    public float minDistance;
    public float stopSpeedThreshold;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        sensor = aGameObject.GetComponent<BeeStingerSensor>();
        shader = aGameObject.GetComponentInChildren<ShaderGraphChangeColor>();
    }

    public override void Enter()
    {
        base.Enter();
        
        shader.ChangeColorGreen();
        
        target = sensor.originalHomepoint;
        rb = sensor.rb;
        
        Quaternion targetRotation = Quaternion.LookRotation(target);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
            5 * Time.fixedDeltaTime));
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        target = sensor.originalHomepoint;

        Vector3 direction = target - transform.position;
        float distance = direction.magnitude;

        if (distance > maxDistance)
        {
            float force = distance * forceMultiplier;
            rb.AddForce(direction.normalized * force);
        }
        else if (distance > minDistance)
        {
            float force = Mathf.Lerp(0, maxDistance * forceMultiplier, distance / maxDistance);
            rb.AddForce(direction.normalized * force);
        }
        else
        {
            rb.velocity = Vector3.zero;
            sensor.backToOrigin = false;
            sensor.seesTarget = false;
            sensor.idle = true;
            Finish();
        }
    }
}