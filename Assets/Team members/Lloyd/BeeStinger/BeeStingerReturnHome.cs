using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class BeeStingerReturnHome : AntAIState
{
    public BeeStingerSensor sensor;
    
    public Vector3 target;
    public Rigidbody rb;
    public float forceMultiplier = 10f;
    public float maxDistance = 10f;
    public float minDistance = 1f;
    public float stopSpeedThreshold = 0.1f;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        sensor = aGameObject.GetComponent<BeeStingerSensor>();
    }

    public override void Enter()
    {
        base.Enter();
        target = sensor.originalHomepoint;
        rb = sensor.rb;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
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
            sensor.idle = true;
        }
        
        if (rb.velocity.magnitude < stopSpeedThreshold)
        {
            rb.velocity = Vector3.zero;
        }
    }
}