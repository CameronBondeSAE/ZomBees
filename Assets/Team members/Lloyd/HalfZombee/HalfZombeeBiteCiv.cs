using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Lloyd;

public class HalfZombeeBiteCiv : AntAIState
{
    public bool pissedOff;

    public float pissedOffFloat;

    public float pissedOffThresh;

    public bool seesCiv;

    public HalfZombeeProfile profile;

    public HalfZombeeSensor sensor;

    public HalfZombeeTurnTowards turnTowards;

    public CivVision vision;

    public SoundEmitter soundEmitter;

    public Rigidbody rb;

    public float attackRadius;
    public float attackDmg;

    public Transform attackPoint;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
        vision = aGameObject.GetComponent<CivVision>();
        profile = aGameObject.GetComponent<HalfZombeeProfile>();
        rb = aGameObject.GetComponent<Rigidbody>();
        soundEmitter = aGameObject.GetComponent<SoundEmitter>();
        sensor = aGameObject.GetComponent<HalfZombeeSensor>();
    }

    public override void Enter()
    {
        base.Enter();
        profile.currentSpeed = profile.walkSpeed;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        seesCiv = vision.seesCivs;

        if (vision.ReturnNearestCiv())
        {
            turnTowards.targetTransform = vision.ReturnNearestCiv();
            CheckDistance();
        }

        else if (!pissedOff || !seesCiv)
            Finish();
    }

    private void CheckDistance()
    {
        Transform attackTarget = vision.ReturnNearestCiv();
        Vector3 targetDirection = attackTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 5 * Time.fixedDeltaTime));

        Ray ray = new Ray(transform.position, attackTarget.position - transform.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, vision.boxSize.z))
        {
            if (hitInfo.collider != attackTarget.GetComponent<Collider>())
            {
                return;
            }

            ICivilian civ = hitInfo.collider.gameObject.GetComponent<ICivilian>();
            if (civ != null)
            {
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
}