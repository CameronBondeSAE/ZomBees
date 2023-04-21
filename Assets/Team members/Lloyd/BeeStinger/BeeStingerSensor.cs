using Anthill.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStingerSensor : MonoBehaviour, ISense
{
    public Rigidbody rb;

    public Vector3 homePoint;

    public Transform viewTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        homePoint = transform.position;
        StartWings();
    }

    #region AntAI
    public bool idle          = false;
    public bool seesTarget        = false;
    public bool heardSound = false;
    public bool sting = false;
    public bool dead = false;
	
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(BeeStingerScenario.Idle, idle);
            aWorldState.Set(BeeStingerScenario.SeesTarget, seesTarget);
            aWorldState.Set(BeeStingerScenario.HeardSound, heardSound);
            aWorldState.Set(BeeStingerScenario.Sting, sting);
            aWorldState.Set(BeeStingerScenario.Dead, dead);
        }
        aWorldState.EndUpdate();
    }
    #endregion

    #region BeeWings

    private BeeWingsManager beeWings;

    private void StartWings()
    {
        beeWings = GetComponentInChildren<BeeWingsManager>();
        beeWings.AddWing();
        beeWings.AddWing();
        beeWings.SpawnWings();
        ChangeWings();
    }

    public void ChangeWings()
    {
        beeWings.OnChangeStatEvent(-90, 15, true);
    }

    #endregion

    #region Attack

    public Transform attackTarget;

    public void SetAttackTarget(Transform newTarget)
    {
        attackTarget = newTarget;
    }

    #endregion
}