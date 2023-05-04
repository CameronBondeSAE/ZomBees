using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Oscar;

public class HalfZombeeBiteCiv : AntAIState
{
    public bool pissedOff;

    public float pissedOffFloat;

    public float pissedOffThresh;

    public bool seesCiv;

    public HalfZombeeProfile profile;

    public HalfZombeeSensor sensor;

    public HalfZombeeTurnTowards turnTowards;

    public SoundEmitter soundEmitter;

    public Rigidbody rb;

    public float attackRadius;
    public float attackDmg;

    public DynamicObject attackTarget;

    public OscarVision vision;

    public HalfZombeeFeelers feelers;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
        profile = aGameObject.GetComponent<HalfZombeeProfile>();
        rb = aGameObject.GetComponent<Rigidbody>();
        soundEmitter = aGameObject.GetComponent<SoundEmitter>();
        sensor = aGameObject.GetComponent<HalfZombeeSensor>();
        vision = sensor.vision;
        feelers = aGameObject.GetComponent<HalfZombeeFeelers>();
    }

    public override void Enter()
    {
        base.Enter();
        pissedOffFloat = 5;
        feelers.enabled = false;
        sensor.beeWings.ChangeBeeWingStats(-100, 55, true);
        profile.currentSpeed = profile.walkSpeed;

        if (vision.civsInSight.Count > 0)
        {
            attackTarget = vision.civsInSight[0];
            turnTowards.targetTransform = attackTarget.transform;
        }

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);



/*.        seesCiv = vision.seesCivs;

        if (vision.ReturnNearestCiv())
        {
            turnTowards.targetTransform = vision.ReturnNearestCiv();
            CheckDistance();
        }*/

        if (attackTarget != null)
            CheckDistance();

        pissedOffFloat -= 0.1f;
      
        if (pissedOffFloat <= 0 || sensor.seesCiv == false)
        {
            Finish();
        }
    }

    private void CheckDistance()
    {
        Vector3 targetDirection = attackTarget.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 5 * Time.fixedDeltaTime));

        Ray ray = new Ray(transform.position, attackTarget.transform.position - transform.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.collider != attackTarget.GetComponent<Collider>())
            {
                return;
            }

            DynamicObject civ = hitInfo.collider.gameObject.GetComponent<DynamicObject>();
            if (civ != null)
            {
                if(civ.isCiv)
                pissedOffFloat++;
            }
        }

        if (pissedOffFloat > pissedOffThresh)
        {
            pissedOff = true;
            Attack();
        }
    }

    public void Attack()
    {
        //make spoopy noise here
        //attack sphere goes here
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Health healthComponent = hitColliders[i].GetComponent<Health>();
            if (healthComponent != null)
            {
                soundEmitter.EmitSound(new SoundProperties(sensor.gameObject, SoundEmitter.SoundType.HalfBee, 15, 0, false, .5f, 1, Team.Bee, 0, "A startling bee attack!"));
                healthComponent.Change(attackDmg);
            }
        }

        pissedOffFloat = 0;
        Finish();
    }

    public override void Exit()
    {
        base.Exit();
        sensor.bitCiv = false;
            pissedOffFloat = 0;
        feelers.enabled = true;
    }
}