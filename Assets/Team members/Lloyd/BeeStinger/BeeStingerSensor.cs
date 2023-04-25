using Anthill.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStingerSensor : MonoBehaviour, ISense
{
    public Rigidbody rb;

    public Vector3 homePoint;
    public Vector3 originalHomepoint;

    public PatrolPoint hivePoint;

    public Transform viewTransform;

    public BeeStingAttack.BeeStingType myType;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        homePoint = transform.position;
        originalHomepoint = homePoint;
        StartWings();
    }

    #region AntAI

    public bool attacking = false;
    public bool idle = false;
    public bool seesTarget = false;
    public bool heardSound = false;
    public bool sting = false;
    public bool dead = false;

    public bool hasResource;
    public bool backToOrigin;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(BeeStingerScenario.Idle, idle);
            aWorldState.Set(BeeStingerScenario.SeesTarget, seesTarget);
            aWorldState.Set(BeeStingerScenario.HeardSound, heardSound);
            aWorldState.Set(BeeStingerScenario.Dead, dead);
            aWorldState.Set(BeeStingerScenario.Attack, attacking);

            aWorldState.Set(BeeStingerScenario.HasResource, hasResource);
            aWorldState.Set(BeeStingerScenario.BackToOrigin, backToOrigin);
        }
        aWorldState.EndUpdate();
    }

    #endregion
    
    #region Attack

    public Transform attackTarget;

    public void SetAttackTarget(Transform newTarget)
    {
        attackTarget = newTarget;
    }

    #endregion

    #region Resources

    public int maxResources;

    public int currentResources;

    public void ChangeResources(int amount)
    {
        currentResources += amount;

        if (currentResources <= 0)
        {
            currentResources = 0;
        }

        if (currentResources >= maxResources)
        {
            currentResources = maxResources;

            hivePoint = PatrolManager.singleton.paths[0];
            homePoint = hivePoint.transform.position;
        }
    }

    #endregion

    #region Wings

    private BeeWingsManager beeWings;
    private void StartWings()
    {
        beeWings = GetComponentInChildren<BeeWingsManager>();
        beeWings.SetWings();
    }

    public void ChangeWings(float angle, float speed, bool alive)
    {
        beeWings.OnChangeStatEvent(angle, speed, alive);
    }

    #endregion
}