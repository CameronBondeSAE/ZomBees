using Anthill.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivSensor : MonoBehaviour, ISense
{
    public AntAIAgent antAIAgent;

    public bool idle = false;

    public bool inRange = false;

    public bool hasInteractTarget = false;
    public bool wantsToInteract = false;

    public bool hungry;
    public bool hasResourceTarget;

    public bool feared;

    public Transform moveTarget;

    public bool hasAttackTarget = false;
    public Transform attackTarget;
    public bool wantsToAttack = false;

    public Transform RotateToTarget;

    public enum MoveType
    {
        Idle,
        WalkToNearestInteract,
        WalkToNearestSafePoint,
        WalkToNearestAttackPoint,
        Searching
    }

    public MoveType myMoveType;

    private void Awake()
    {
        StartBeeParts();
    }

    public void Update()
    {
        if (idle)
        {
            myMoveType = MoveType.Idle;
        }

        else if (wantsToInteract && !hasInteractTarget)
        {
            myMoveType = MoveType.Searching;
        }

        else if (wantsToInteract)
        {
            myMoveType = MoveType.WalkToNearestInteract;
        }

        else if (hungry && !hasResourceTarget)
        {
            myMoveType = MoveType.Searching;
        }

        else if (feared)
        {
            myMoveType = MoveType.WalkToNearestSafePoint;
        }

        else if (hasAttackTarget && wantsToAttack)
        {
            myMoveType = MoveType.WalkToNearestAttackPoint;
        }
    }

    public void ChangeRotateTarget(Transform newTarget)
    {
        RotateToTarget = newTarget;
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(NormalCivScenario.InRange, inRange);

            aWorldState.Set(NormalCivScenario.HasInteractTarget, hasInteractTarget);
            aWorldState.Set(NormalCivScenario.WantsToInteract, wantsToInteract);

            aWorldState.Set(NormalCivScenario.HasAttackTarget, hasAttackTarget);
            aWorldState.Set(NormalCivScenario.WantsToAttack, wantsToAttack);

            aWorldState.Set(NormalCivScenario.Idle, idle);
        }
        aWorldState.EndUpdate();
    }

    public void DesireToInteract()
    {
        wantsToInteract = true;
    }

    public void BecomeEgg()
    {
        EggManager.instance.StartEgg(gameObject);
    }

    #region Head

    private BeePartsManager beeparts;

    private void StartBeeParts()
    {
        beeparts = GetComponentInChildren<BeePartsManager>();

        beeparts.HumanEyes();
        beeparts.LoseAntannae();
        beeparts.LoseMandibles();
    }

    #endregion
}