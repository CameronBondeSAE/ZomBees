using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using Team_members.Lloyd.CivFinal;
using Team_members.Lloyd.Civilian_L;
using UnityEngine;
using UnityEngine.AI;
public class NormalCivMoveToTarget : AntAIState
{
    //swiped from tutorial folder
    
    public GameObject owner;
    //NavMeshAgent      navMeshAgent;

    public Rigidbody rb;
    
    public Transform target;
            
    //get from stats
    public float moveSpeed = 25f;
    public float stopDistance = 1f;
    public float decelerationDistance;
            
    public bool inRange;

    private CivSensor sensor;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner        = aGameObject;
        //navMeshAgent = owner.GetComponent<NavMeshAgent>();

        rb = owner.GetComponent<Rigidbody>();

        sensor = owner.GetComponent<CivSensor>();
    }

    public override void Enter()
    {
        base.Enter();

        // navMeshAgent.SetDestination(owner.GetComponent<NormalCivBrain>().moveTarget.transform.position);
       decelerationDistance = stopDistance * 3;

       inRange = false;
       
       target = sensor.moveTarget;
       sensor.ChangeRotateTarget(target);
    }

    private void FixedUpdate()
            {
    
                if (target == null) return;
    
                Vector3 direction = target.position - transform.position;
                float distance = direction.magnitude;
    
                if (distance <= stopDistance)
                {
                    Stop();
                }
                
                else if (distance <= decelerationDistance)
                {
                    float decelerationFactor = Mathf.Clamp01((distance - stopDistance) / (decelerationDistance - stopDistance));
                    rb.AddForce(direction.normalized * moveSpeed * decelerationFactor, ForceMode.Acceleration);
                }
                else
                {
                    rb.AddForce(direction.normalized * moveSpeed, ForceMode.Acceleration);
                }
            }
    
            private void Stop()
            {
                rb.velocity = Vector3.zero;
                inRange = true;
                owner.GetComponent<CivSensor>().inRange = inRange;
                Finish();
            }
    
            public override void Exit()
            {
                base.Exit();
                inRange = false; 
            }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        //if (navMeshAgent.remainingDistance < 1f)
        {
          //  owner.GetComponent<NormalCivBrain>().moveTarget = null;
            Finish();
        }
    }
}