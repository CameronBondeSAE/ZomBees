using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class QueenAttack : AntAIState
{
    QueenEvent queenEvent;

    private QueenScenarioManager queenScene;

    private List<GameObject> targets;

    private int numTargets;

    private List<GameObject> followers;

    private int numFollowers;

    public float timeBetweenAttacks;

    public bool playerTracked = true;

    public bool attacking;

    public float attackSize;
    
    public override void Enter()
    {
        queenEvent = GetComponent<QueenEvent>();

        queenScene = GetComponent<QueenScenarioManager>();

        //queenScene.attacking = true;
        StartCoroutine(AttackPeriodically());
    }

    private void FixedUpdate()
    {
        //playerTracked = queenScene.spottedHuman;
        //queenScene.attacking = true;
    }

    private IEnumerator AttackPeriodically()    
    {
        while (attacking)
        {
            targets = queenScene.harvestTargets;
            followers = queenScene.followers;

            for (int i = 0; i < targets.Count; i++)
            {
                GameObject target = targets[i];

                GameObject follower = followers[i % followers.Count];

                queenEvent.OnChangeSwarmPoint(target.transform);
                queenEvent.OnChangeSwarmCircleSize(attackSize);
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    public override void Exit()
    {
        
    }
}