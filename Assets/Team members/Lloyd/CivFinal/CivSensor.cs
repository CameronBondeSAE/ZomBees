using Anthill.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivSensor : MonoBehaviour, ISense
{
    public AntAIAgent antAIAgent;

    public bool idle = false;

    public bool inRange          = false;
    public bool hasInteractTarget        = false;
    public bool wantsToInteract = false;
    
    public Transform moveTarget;

    public bool hasAttackTarget = false;
    public Transform attackTarget;
    public bool wantsToAttack = false;

    public Transform RotateToTarget;

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
}