using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class QueenAttack : AntAIState
{
    QueenEvent queenEvent;

    private QueenScenarioManager scenManager;

    private List<GameObject> targets;

    private int numTargets;

    private List<GameObject> followers;

    private int numFollowers;

    public float timeBetweenAttacks;

    public override void Enter()
    {
        queenEvent = GetComponent<QueenEvent>();

        scenManager = GetComponent<QueenScenarioManager>();
    }

    private void Update()
    {
        targets = scenManager.harvestTargets;
        followers = scenManager.followers;
    }

    private IEnumerator AttackPeriodically()
    {
        while (true)
        {
            // Get the current lists of targets and followers
            targets = scenManager.harvestTargets;
            followers = scenManager.followers;

            for (int i = 0; i < targets.Count; i++)
            {
                GameObject target = targets[i];

                GameObject follower = followers[i % followers.Count];

                follower.SendMessage("ReceiveTarget", target);
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    // public override void Exit()
    // {
    //     
    // }
}